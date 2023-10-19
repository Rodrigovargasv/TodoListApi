
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infra.Data.Context;

namespace TodoList.Infra.Data.Repository
{
    public class RecoveryPasswordRepository : IRecoveryPasswordUserRepository
    {

        private readonly ApplicationDbContext _context;


        public RecoveryPasswordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> VerificationUser(string email)
            => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            
        
    }
}
