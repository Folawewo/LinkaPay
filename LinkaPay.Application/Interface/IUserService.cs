using System;
using LinkaPay.Application.ServiceModels.Responses;
using LinkaPay.Application.ViewModels;

namespace LinkaPay.Application.Interface
{
    public interface IUserService
    {
        Task<Tuple<GenericResponseViewModel<UserViewModel>, ErrorResponse>> CreateUser(CreateUserRequest model);
    }
}

