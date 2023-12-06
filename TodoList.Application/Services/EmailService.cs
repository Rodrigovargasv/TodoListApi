
using eSistemCurso.Domain.Common.Exceptions;
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
        {
            var emailId = await _email.GetEmailByIdAsync(id);

            if (emailId == null)
                throw new NotFoundException("Email não encontrado.");

            if (id != emailId.Id)
                throw new BadRequestException("Os dados informado estão incorretos.");

            return emailId;
        }


        public async Task<EmailUser> UpdateEmailAsync(int id, EmailUser email)
        {

            var jobValidation = await _emailValidation.ValidateAsync(email);
            if (!jobValidation.IsValid) throw new ValidationException(jobValidation.Errors);

            if (id == 1)
                return await _email.UpdateEmailAsync(email);
            else
                throw new NotFoundException("Id não encontrado.");
      
        }



    }
}
