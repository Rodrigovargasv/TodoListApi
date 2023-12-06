
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface IRecoveryPasswordUserService
    {
        Task<string> VerificationUserAsync(string email);
    }
}
