
using TodoList.Domain.Validation;

namespace TodoList.Domain.Entities
{
    public class Job
    {
        public Job(int id, string name, string description, DateTime createDate, DateTime executionDate, TaskStatus taskStatus)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name),
                "O campo nome tem preenchimento obrigatório");

            DomainExceptionValidation.When(createDate > DateTime.Now, "A data de criação da tarefa deve estar no passado."); CreateDate = DateTime.Now;

            Id = id;
            Name = name;
            Description = description;
            CreateDate = DateTime.Now;
            ExecutionDate = executionDate;
            TaskStatus = taskStatus;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public DateTime CreateDate { get; }

        public DateTime ExecutionDate { get; private set; }

        public TaskStatus TaskStatus { get; private set; }

    }
}
