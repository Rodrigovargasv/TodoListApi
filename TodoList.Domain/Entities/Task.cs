
using TodoList.Domain.ValueObject;

namespace TodoList.Domain.Entities
{
    public class Task
    {
        public Task(int id, string name, string description, Date date, TaskStatus taskStatus)
        {
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
