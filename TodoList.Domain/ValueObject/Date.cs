
namespace TodoList.Domain.ValueObject
{
    public class Date
    {
        public Date(DateTime createDate, DateTime executionDate)
        {
            CreateDate = createDate;
            ExecutionDate = executionDate;
        }

        public DateTime CreateDate { get; }

        public DateTime ExecutionDate { get; }
    }
}
