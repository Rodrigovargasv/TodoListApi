
using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces
{
    public interface IEmailRespository
    {
        Task<Email> GetEmailByIdAsync(int? id);
        Task<Email> UpdateEmailAsync(Email email);
    
    }
}
