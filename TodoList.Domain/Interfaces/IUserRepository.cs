
using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User user);

        Task DeleteUser(User User);
        

        
    }
}
