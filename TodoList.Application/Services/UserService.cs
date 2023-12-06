
using eSistemCurso.Domain.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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

            var userId = await GetLoginAndEmailAync(user.UserName, user.Email);

            if (userId != null)
                throw new BadRequestException("Já existe um usuário com estás informações de Nome ou Email");

            return await _userRepository.CreateUser(user);
        }

        public async Task<string> DeleteUserAsync(int id)
        {
            var userId = await GetUserByIdAsync(id);

            if (userId is null)
                throw new NotFoundException($"Não foi encontrando o usuario com id: {id}.");

            await _userRepository.DeleteUser(userId);

            return $"Id: {id}, foi removido com sucesso.";
        }


        public async Task<IEnumerable<User>> GetAllUsersAync()
        {

            var users = await _userRepository.GetAllUsers();

            if (users is null)
                throw new NotFoundException("Usuários não encontrados.");
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {

            var userId = await _userRepository.GetUserById(id);

            if (userId is null)
                throw new NotFoundException($"Usuario com id: {id} não foi encontrando.");

            return userId;
        }

        public async Task<User> UpdateUserAsync(User user, int id)
        {

            var userId = await GetUserByIdAsync(id);

            var userExists = await GetLoginAndEmailAync(user.UserName, user.Email);

            if (userId is null)
                throw new NotFoundException($"Não foi encontrando o usuario com id: {id}.");
            if (user is null)
                throw new BadRequestException("Dados inválidos.");

            if (userExists != null)
                throw new BadRequestException("Não foi possível atualizar o usuário, pois já existe um usuário com estes dados.");

            var userValidation = await _userValidation.ValidateAsync(user);

            if (!userValidation.IsValid) throw new ValidationException(userValidation.Errors);

            return await _userRepository.UpdateUser(user);
        }

        public async Task<string> GetLoginAndPassWordAsync(string userName, string password)
        {
            var user = await _userRepository.GetLoginAndPassWord(userName.ToLower(), password);

            if (user is null)
                throw new BadRequestException("Usuário ou senha inválido.");

            if (user.IsActive == false)
                throw new CustomException("Usuário está inativo ou inválido.");

            return _tokenService.GenerateToken(user);
        }

        public async Task<User> GetLoginAndEmailAync(string userName, string email)
            => await _userRepository.GetLoginAndEmail(userName, email);

    }
}
