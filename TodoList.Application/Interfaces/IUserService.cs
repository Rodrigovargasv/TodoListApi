using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> CreateUserAsync(User user);

        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);

        Task<User> GetLoginAndPassWordAsync(string userName, string password);

        Task<User> GetLoginAndEmailAync(string userName, string email);
    }
}
