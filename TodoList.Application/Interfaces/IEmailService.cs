
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface IEmailService
    {

        Task<Email> GetEmailByIdAsync(int? id);
        Task<Email> UpdateEmailAsync(Email email);

    }
}
