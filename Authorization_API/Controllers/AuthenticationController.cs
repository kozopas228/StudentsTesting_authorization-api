using Authorization_Models;
using Authorization_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Authorization_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string login, string password)
        {
            var token = await _authenticationService.Login(login, password);

            if (String.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Content(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string login, string password, string firstName, string lastName)
        {
            var user = new User { FirstName = firstName, LastName = lastName, Login = login, Password = password };

            var result = await _authenticationService.Register(user);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
