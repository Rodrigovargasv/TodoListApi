

using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services
{
    public class UserService : IUserService
    {

        private  readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
           => _userRepository = userRepository;
        

        public async Task<User> CreateUserAsync(User user)
        
           => await _userRepository.CreateUser(user);

        public async Task DeleteUserAsync(User user)
            => await _userRepository.DeleteUser(user);

        public async Task<IEnumerable<User>> GetAllUsersAync()
            => await _userRepository.GetAllUsers();

        public async Task<User> GetUserByIdAsync(int id)
            => await _userRepository.GetUserById(id);

        public Task<User> UpdateUserAsync(User user)
            => _userRepository.UpdateUser(user);
    }
}
