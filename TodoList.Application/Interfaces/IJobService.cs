
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface IJobService
    {
        Task<int>CreateJobAync(Job job);
        Task<IEnumerable<Job>> GetAllJobAsync();
        Task<Job> GetJobByIdAsync(int? id);
        Task<string> UpdateJobAsync(Job job, int id);
        Task<string> DeleteJobAsync(int? id);
    }
}
