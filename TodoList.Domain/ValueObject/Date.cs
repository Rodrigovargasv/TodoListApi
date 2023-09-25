
using TodoList.Domain.Validation;

namespace TodoList.Domain.ValueObject
{
    public class Date
    {
        public Date(DateTime createDate, DateTime executionDate)
        {

            DomainExceptionValidation.When(createDate > DateTime.Now, "A data de criação da tarefa deve estar no passado.");

            CreateDate = createDate;
            ExecutionDate = executionDate;

           
        }

        public DateTime CreateDate { get; }

        public DateTime ExecutionDate { get; }


     
    }
}
