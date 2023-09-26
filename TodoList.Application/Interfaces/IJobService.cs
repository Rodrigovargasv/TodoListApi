
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface IJobService
    {
        Task CreateJobAync(Job job);
        Task<IEnumerable<Job>> GetAllJobAsync();
        Task<Job> GetJobByIdAsync(int? id);
        Task UpdateJobAsync(Job job);
        Task DeleteJobAsync(int? id);
    }
}
