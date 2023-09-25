
using TodoList.Domain.Validation;
using TodoList.Domain.ValueObject;

namespace TodoList.Domain.Entities
{
    public class Job
    {
        public Job(int id, string name, string description, Date date, TaskStatus taskStatus)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name),
                "O campo nome tem preenchimento obrigatorio");

            Id = id;
            Name = name;
            Description = description;
            Date = date;
            this.TaskStatus = taskStatus;

           
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public Date Date { get; private set; } 

        public TaskStatus TaskStatus { get; }

    }
}
