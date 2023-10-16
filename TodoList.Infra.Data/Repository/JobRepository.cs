
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infra.Data.Context;

namespace TodoList.Infra.Data.Repository
{
    public class JobRepository : IJobRepository
    {

        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Job> CreateJobAync(Job job)
        {
            _context.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<Job> DeleteJobAsync(Job job)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<IEnumerable<Job>> GetAllJobAsync()
        {
            return await _context.Jobs.OrderBy(x => x.Id)
                .ToListAsync();
            
        }

        public async Task<Job> GetJobByIdAsync(int? id)
        {
           return await _context.Jobs.FindAsync(id);
            
        }

        public async Task<Job> UpdateJobAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();

            return job;
        }
    }
}
