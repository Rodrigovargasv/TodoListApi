
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface IEmailService
    {

        Task<EmailUser> GetEmailByIdAsync(int? id);
        Task<EmailUser> UpdateEmailAsync(EmailUser email);

    }
}
