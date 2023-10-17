
using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces
{
    public interface IEmailRepository
    {
        Task<Email> GetEmailByIdAsync(int? id);
        Task<Email> UpdateEmailAsync(Email email);
    
    }
}
