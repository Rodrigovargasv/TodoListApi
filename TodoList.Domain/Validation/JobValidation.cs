
using FluentValidation;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Validation
{
    public class JobValidation : AbstractValidator<Job>
    {
        public JobValidation() 
        {
            RuleFor(j => j.Name)
             .NotEmpty()
             .NotNull()
             .MinimumLength(3);
        }
    }
}
