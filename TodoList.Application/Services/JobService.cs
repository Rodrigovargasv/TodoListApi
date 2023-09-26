
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services
{
    public class JobService : IJobService
    {

        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task CreateJobAync(Job job)
        {
            await _jobRepository.CreateJobAync(job);
        }

        public async Task DeleteJobAsync(int? id)
        {
            var jobbId = _jobRepository.GetJobByIdAsync(id).Result;
            await _jobRepository.DeleteJobAsync(jobbId);

        }

        public Task<IEnumerable<Job>> GetAllJobAsync()
        {
            return  _jobRepository.GetAllJobAsync();
        }

        public Task<Job> GetJobByIdAsync(int? id)
        {
            return _jobRepository.GetJobByIdAsync(id);
        }

        public Task UpdateJobAsync(Job job)
        {
            return _jobRepository.UpdateJobAsync(job);

        }
    }
}
