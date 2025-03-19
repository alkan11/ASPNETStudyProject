using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IAuthendicationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto model);
        Task<bool> ValideUser(UserForauthendicationDto model);
        Task<TokenDto> CreateToken(bool populateExpire);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}
