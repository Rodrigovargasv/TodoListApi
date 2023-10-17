﻿
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services
{
    public class EmailService : IEmailService
    {

        private readonly IEmailRepository _email;

        public EmailService(IEmailRepository email)
        {
            _email = email;
        }

        public async Task<Email> GetEmailByIdAsync(int? id)
           => await _email.GetEmailByIdAsync(id);
        

        public async Task<Email> UpdateEmailAsync(Email email)
            => await _email.UpdateEmailAsync(email);
        
            
        
    }
}
