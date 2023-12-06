
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Validation;
using FluentValidation;
using eSistemCurso.Domain.Common.Exceptions;


namespace TodoList.Application.Services
{
    public class JobService : IJobService
    {

        private readonly IJobRepository _jobRepository;
        private readonly JobValidation _validationsJob;

        public JobService(IJobRepository jobRepository, JobValidation validationRules)
        {
            _jobRepository = jobRepository;
            _validationsJob = validationRules;
        }

        public async Task<int> CreateJobAync(Job job)
        {

            var jobValidation = await _validationsJob.ValidateAsync(job);
            if (!jobValidation.IsValid) throw new ValidationException(jobValidation.Errors);

            var newJob = await _jobRepository.CreateJobAync(job);

            return newJob.Id;
        }


        public async Task<string> DeleteJobAsync(int? id)
        {

            var jobId = await GetJobByIdAsync(id);

            if (jobId is null)
                throw new NotFoundException("Tarefa não encontrada.");

            var job = await _jobRepository.DeleteJobAsync(jobId);

            return $"Tarefa com id: {id}, foi deletada com sucesso.";

        }

        public async Task<IEnumerable<Job>> GetAllJobAsync()
        {

            var jobs = await _jobRepository.GetAllJobAsync();

            if (jobs is null)
                throw new NotFoundException("Tarefas não encontradas.");

            return jobs;


        }


        public async Task<Job> GetJobByIdAsync(int? id)
        {

            var jobId = await _jobRepository.GetJobByIdAsync(id);

            if (jobId is null)
                throw new NotFoundException("Id não encontrado.");

            return jobId;

        }


        public async Task<string> UpdateJobAsync(Job job, int id)
        {

            if (job is null || id != job.Id)
                throw new BadRequestException("Tarefa não encontrada, os dados informados estão incorretos.");

            var jobValidation = await _validationsJob.ValidateAsync(job);
            if (!jobValidation.IsValid) throw new ValidationException(jobValidation.Errors);

            var jobId = await _jobRepository.UpdateJobAsync(job);

            return $"A tarefa com id: {jobId.Id}, foi atualiza com sucesso.";
        }





    }
}
