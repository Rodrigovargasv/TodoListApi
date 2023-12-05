
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Validation
{
    public class EmailValidation : AbstractValidator<EmailUser>
    {

        public EmailValidation()
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
