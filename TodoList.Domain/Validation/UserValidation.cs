

using FluentValidation;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation() 
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(80);

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$!%^&*])[A-Za-z\d@#$!%^&*]{8,}$")
                .WithMessage("A senha deve conter no mínimo 1 letra maiúscula, 1 letra minúsculas, 1 caracter especial e 8 caracteres.");
        }
    }
}
