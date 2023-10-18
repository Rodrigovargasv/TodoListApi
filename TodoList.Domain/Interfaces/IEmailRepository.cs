
using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces
{
    public interface IEmailRepository
    {
        Task<EmailUser> GetEmailByIdAsync(int? id);
        Task<EmailUser> UpdateEmailAsync(EmailUser email);
    
    
    }
}
