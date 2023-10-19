using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces
{
    public interface IRecoveryPasswordUserRepository
    {
        Task<User> VerificationUser(string email);
    }
}
