
using FluentValidation;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Validation;

namespace TodoList.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly UserValidation _userValidation;

        public UserService(IUserRepository userRepository, UserValidation validationRules)
        {
            _userRepository = userRepository;
            _userValidation = validationRules;
          
        }


        public async Task<User> CreateUserAsync(User user)
        {
            var userValidation = await _userValidation.ValidateAsync(user);

            if (!userValidation.IsValid) throw new ValidationException(userValidation.Errors);

            return await _userRepository.CreateUser(user);
        }

        public async Task DeleteUserAsync(User user)
            => await _userRepository.DeleteUser(user);

        public async Task<IEnumerable<User>> GetAllUsersAync()
            => await _userRepository.GetAllUsers();

        public async Task<User> GetUserByIdAsync(int id)
            => await _userRepository.GetUserById(id);

        public async Task<User> UpdateUserAsync(User user)
        {

            var userValidation = await _userValidation.ValidateAsync(user);

            if (!userValidation.IsValid) throw new ValidationException(userValidation.Errors);

            return await _userRepository.UpdateUser(user);
        }

        public async Task<User> GetLoginAndPassWordAsync(string userName, string password)
            => await _userRepository.GetLoginAndPassWord(userName, password);

        public async Task<User> GetLoginAndEmailAync(string userName, string email)
            => await _userRepository.GetLoginAndEmail(userName, email);

    }
}
