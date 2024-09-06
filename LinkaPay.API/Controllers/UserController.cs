using System;
using LinkaPay.Application.Interface;
using LinkaPay.Application.ServiceModels.Responses;
using LinkaPay.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LinkaPay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
	{
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(GenericResponseViewModel<UserViewModel>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var (response, error) = await _userService.CreateUser(request);

            if (error != null)
            {
                return BadRequest(error.ErrorMessage);
            }
            return Ok(response);
        }
    }
}

