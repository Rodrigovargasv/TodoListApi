
using eSistemCurso.Domain.Common.Exceptions;
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

        private readonly TokenService _tokenService;

        public UserService(IUserRepository userRepository, UserValidation validationRules, TokenService tokenService)
        {
            _userRepository = userRepository;
            _userValidation = validationRules;
            _tokenService = tokenService;
          
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

        public async Task<string> GetLoginAndPassWordAsync(string userName, string password)
        {
           var user = await _userRepository.GetLoginAndPassWord(userName, password);

            if (user is null)
                throw new BadRequestException("Usuário ou senha inválido");

            if (user.IsActive == false)
                throw new CustomException("Usuário está inativo ou inválido");

            return _tokenService.GenerateToken(user);
        }

        public async Task<User> GetLoginAndEmailAync(string userName, string email)
            => await _userRepository.GetLoginAndEmail(userName, email);

    }
}
