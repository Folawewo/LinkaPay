using System;
using System.Threading.Tasks;
using LinkaPay.Application.Interface;
using LinkaPay.Application.Security;
using LinkaPay.Application.ServiceModels.Responses;
using LinkaPay.Application.ViewModels;
using LinkaPay.Domain.Entities;
using LinkaPay.Domain.IRepository;

namespace LinkaPay.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _userRepository;

        public UserService(IRepository<Users> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Tuple<GenericResponseViewModel<UserViewModel>, ErrorResponse>> CreateUser(CreateUserRequest model)
        {
            var existingUser = await _userRepository.GetUserByEmail(model.Email);
            if (existingUser != null)
            {
                return new Tuple<GenericResponseViewModel<UserViewModel>, ErrorResponse>(
                    null,
                    new ErrorResponse { ErrorMessage = "User already exists" }
                );
            }

            var user = new Users
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.Email,
                HashedPassword = PasswordHasher.HashPassword(model.Password),
                CreatedBy = "System"
            };

            await _userRepository.AddUser(user);

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.EmailAddress
            };

            return new Tuple<GenericResponseViewModel<UserViewModel>, ErrorResponse>(
                new GenericResponseViewModel<UserViewModel>
                {
                    code = "201",
                    message = "User created successfully",
                    IsSuccessful = true,
                    data = userViewModel
                },
                null
            );
        }

    }
}
