using AutoMapper;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Services
{
    public class AuthendicationManager : IAuthendicationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User? _user;
        public AuthendicationManager( IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<TokenDto> CreateToken(bool populateExpire)
        {
            var signinCredentials = GetSigninCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken= refreshToken;

            if (populateExpire)
            {
                _user.RefreshTokenExpire = DateTime.Now.AddMinutes(10);
            }
            await _userManager.UpdateAsync(_user);
            var accessToken= new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto model)
        {
            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, model.Roles);
            }

            return result;
        }

        public async Task<bool> ValideUser(UserForauthendicationDto model)
        {
            _user = await _userManager.FindByNameAsync(model.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user,model.Password));

            if (!result)
            {
                Console.WriteLine("Authentication failed.");
            }

            return result;
        }


        private SigningCredentials GetSigninCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);

            var secret = new SymmetricSecurityKey(key);
             
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,_user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials:signinCredentials
                );
            return tokenOptions;
        }


        private string GenerateRefreshToken()
        {
            var randomNumber=new byte[32];
            using (var rng=RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        } 

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSetting = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSetting["secretKey"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,//Bu keyi kim ürettiyse bunu doğrula
                ValidateAudience = true,//geçerli bir alıcımı değil mi doğrula
                ValidateLifetime = true,//geçerliliğini doğrula
                ValidateIssuerSigningKey = true,//anahtarı doğrulamak için kullanılır
                ValidIssuer = jwtSetting["validIssuer"],
                ValidAudience = jwtSetting["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var tokenHandler= new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecuritytoken=securityToken as JwtSecurityToken;
            if(jwtSecuritytoken is null || jwtSecuritytoken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            
            if(user is null || user.RefreshToken!=tokenDto.RefreshToken || user.RefreshTokenExpire<=DateTime.Now)
            {
                throw new RefreshTokenBadRequestException();
            }

            _user = user;
            return await CreateToken(false);
        }
    }
}
