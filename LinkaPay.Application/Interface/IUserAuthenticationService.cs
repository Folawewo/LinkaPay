using System;
using LinkaPay.Application.ServiceModels.Requests;
using LinkaPay.Application.ServiceModels.Responses;

namespace LinkaPay.Application.Interface
{
	public interface IUserAuthenticationService
	{
        Task<Tuple<string, string, ErrorResponse>> Authenticate(UserLoginRequest model);
    }
}

