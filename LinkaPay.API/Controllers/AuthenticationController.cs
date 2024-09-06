using LinkaPay.Application.Interface;
using LinkaPay.Application.ServiceModels.Requests;
using LinkaPay.Application.ServiceModels.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LinkaPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService _authService;

        public AuthenticationController(IUserAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var (token, email, error) = await _authService.Authenticate(request);

            if (error != null)
            {
                return BadRequest(error.ErrorMessage);
            }

            return Ok(new { Token = token, Email = email });
        }
    }
}
