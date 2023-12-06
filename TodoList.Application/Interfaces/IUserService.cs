using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> CreateUserAsync(User user);

        Task<User> UpdateUserAsync(User user, int id);
        Task<string> DeleteUserAsync(int id);

        Task<string> GetLoginAndPassWordAsync(string userName, string password);

        Task<User> GetLoginAndEmailAync(string userName, string email);
    }
}
