
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infra.Data.Context;

namespace TodoList.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<User>> GetAllUsers()
            => await _context.Users.OrderBy(x => x.Id).ToListAsync();


        public async Task<User> GetUserById(int id)
            => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);


        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetLoginAndPassWord(string userName, string password)
            => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);

        public async Task<User> GetLoginAndEmail(string userName, string email)
            => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName || x.Email == email);


    }
}
