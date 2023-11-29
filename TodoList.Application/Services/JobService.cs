
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Validation;
using FluentValidation;

namespace TodoList.Application.Services
{
    public class JobService : IJobService
    {

        private readonly IJobRepository _jobRepository;
        private readonly JobValidation _validationsJob;

        public JobService(IJobRepository jobRepository, JobValidation validationRules )
        {
            _jobRepository = jobRepository;
            _validationsJob = validationRules;
        }

        public async Task CreateJobAync(Job job)
        {
            var jobValidation = await _validationsJob.ValidateAsync(job);
            if (!jobValidation.IsValid) throw new ValidationException(jobValidation.Errors);

            await _jobRepository.CreateJobAync(job);
        }


        public async Task DeleteJobAsync(int? id)
        {
            var jobbId = await _jobRepository.GetJobByIdAsync(id);
            await _jobRepository.DeleteJobAsync(jobbId);

        }

        public async Task<IEnumerable<Job>> GetAllJobAsync()
            => await _jobRepository.GetAllJobAsync();


        public async Task<Job> GetJobByIdAsync(int? id)
           => await _jobRepository.GetJobByIdAsync(id);


        public async Task UpdateJobAsync(Job job)
            => await _jobRepository.UpdateJobAsync(job);





    }
}
