using System;
namespace LinkaPay.Application.ServiceModels.Requests
{
    public class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

