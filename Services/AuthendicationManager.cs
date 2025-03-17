using AutoMapper;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.Abstract;


namespace Services
{
    public class AuthendicationManager : IAuthendicationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthendicationManager( IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
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
    }
}
