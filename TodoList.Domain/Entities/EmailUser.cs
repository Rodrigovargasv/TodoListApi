﻿
using System.Net.Mail;
using TodoList.Domain.Validation;

namespace TodoList.Domain.Entities
{
    public class EmailUser
    {
        public EmailUser(int id, string? email)
        {
            Id = id;
            ValidationDomainEmail(email);
        }

        public int Id { get; private set; }

        public string? Email {get; private set;}


        private bool EmailIsValid(string email)
        {
            try
            {
                var validateEmail = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public void ValidationDomainEmail(string email)
        {
            
            DomainExceptionValidation.When(EmailIsValid(email) is false, "O email não está em um formato válido");

            Email = email;


        }

    }

   
}