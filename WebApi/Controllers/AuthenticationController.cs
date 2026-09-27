using Business.Abstract.UnitOfWorks;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public AuthenticationController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterAsync([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var result = await _manager.AuthenticationService.RegisterUser(userForRegistrationDto);

            if (result.Succeeded == false)
            {
                foreach (var registration in result.Errors)
                {
                    ModelState.TryAddModelError(registration.Code, registration.Description);
                }
                return BadRequest(ModelState);
            }

            return Created();
        }
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            if (!await _manager.AuthenticationService.ValidateUser(userForAuthenticationDto))
            {
                return Unauthorized();
            }
            var tokenDto = await _manager.AuthenticationService.CreateToken(populateExp:
                true);

            return Ok(tokenDto);
        }
        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _manager.AuthenticationService.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }
    }
}
