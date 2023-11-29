using TodoList.Domain.Enums;

namespace TodoList.Domain.Entities
{
    public class Job
    {
     
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime ExecutionDate { get; set; }

        public JobStatus? JobStatus { get; set; }

    }
}
