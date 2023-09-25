
namespace TodoList.Domain.Entities
{
    public class Task
    {
        public Task(int id, string name, string description, DateTime createDate, DateTime executionDate, TaskStatus taskStatus)
        {
            Id = id;
            Name = name;
            Description = description;
            CreateDate = createDate;
            ExecutionDate = executionDate;
            this.taskStatus = taskStatus;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public DateTime CreateDate { get; private set; }

        public DateTime ExecutionDate { get; private set; }

        public TaskStatus taskStatus { get; }




    }
}
