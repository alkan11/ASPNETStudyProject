using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Presentation.ActionFilter;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController:ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthenticationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }



        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser(UserForRegistrationDto model)
        {
            var result=await _serviceManager.AuthendicationService.RegisterUser(model);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                    return BadRequest(ModelState);

                }
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authentication(UserForauthendicationDto model)
        {
            if(!await _serviceManager.AuthendicationService.ValideUser(model))
            {
                return Unauthorized();
            }
            var tokenDto = await _serviceManager.AuthendicationService.CreateToken(true);
            return Ok(tokenDto);
        }


        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh(TokenDto tokenDto)
        {
            var tokenDtoReturn=await _serviceManager.AuthendicationService.RefreshToken(tokenDto);
            return Ok(tokenDtoReturn);
        }

    }
}
