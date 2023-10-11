
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
           => await _jobRepository.CreateJobAync(job);
        

        public async Task DeleteJobAsync(int? id)
        {
            var jobbId = _jobRepository.GetJobByIdAsync(id).Result;
            await _jobRepository.DeleteJobAsync(jobbId);

        }

        public async Task<IEnumerable<Job>> GetAllJobAsync()
            => await _jobRepository.GetAllJobAsync();
        

        public async Task<Job> GetJobByIdAsync(int? id)
           =>  await _jobRepository.GetJobByIdAsync(id);
        

        public async Task UpdateJobAsync(Job job)
            => await _jobRepository.UpdateJobAsync(job);

        
    }
}
