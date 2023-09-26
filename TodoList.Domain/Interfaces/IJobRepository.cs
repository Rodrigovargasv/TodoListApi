
using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces
{
    public interface IJobRepository
    {
        Task<Job> CreateJobAync(Job job);
        Task<IEnumerable<Job>> GetAllJobAsync();
        Task<Job> GetJobByIdAsync(int? id);
        Task<Job> UpdateJobAsync(Job job);
        Task<Job> DeleteJobAsync(Job job);

    }
}
