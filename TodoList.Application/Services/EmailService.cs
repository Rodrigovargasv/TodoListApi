
using FluentValidation;
using Hangfire.Common;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Validation;

namespace TodoList.Application.Services
{
    public class EmailService : IEmailService
    {

        private readonly IEmailRepository _email;
        private readonly EmailValidation _emailValidation;

        public EmailService(IEmailRepository email, EmailValidation validationRules)
        {
            _email = email;
            _emailValidation = validationRules;
        }

        public async Task<EmailUser> GetEmailByIdAsync(int? id)
           => await _email.GetEmailByIdAsync(id);
        

        public async Task<EmailUser> UpdateEmailAsync(EmailUser email)
        {

            var jobValidation = await _emailValidation.ValidateAsync(email);
            if (!jobValidation.IsValid) throw new ValidationException(jobValidation.Errors);

            return await _email.UpdateEmailAsync(email);
        }
        
            
        
    }
}
