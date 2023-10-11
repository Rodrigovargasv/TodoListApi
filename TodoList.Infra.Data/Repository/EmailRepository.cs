

using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infra.Data.Context;

namespace TodoList.Infra.Data.Repository
{
    public class EmailRepository : IEmailRespository
    {
        private readonly ApplicationDbContext _context;

        public EmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Email> GetEmailByIdAsync(int? id)
        {
            return await _context.Emails.FindAsync(id);
        }

        public async Task<Email> UpdateEmailAsync(Email email)
        {
            _context.Emails.Update(email);
            await _context.SaveChangesAsync();
            return email;

        }
    }
}
