
using TodoList.Domain.Enums;
using TodoList.Domain.Validation;

namespace TodoList.Domain.Entities
{
    public class Job
    {
        public Job(int id, string name, string description, DateTime createDate, DateTime executionDate, JobStatus jobStatus)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name),
                "O campo nome tem preenchimento obrigatório");




            Id = id;
            Name = name;
            Description = description;
            CreateDate = DateTime.Now;
            ExecutionDate = executionDate;
            JobStatus = jobStatus;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public DateTime CreateDate { get; }

        public DateTime ExecutionDate { get; private set; }

        public JobStatus JobStatus { get; private set; }

    }
}
