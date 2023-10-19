

using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface IRecoveryPasswordUserService
    {
        Task<User> VerificationUserAsync(string email);
    }
}
