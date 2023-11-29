
using System.Net.Mail;
using System.Text.RegularExpressions;
using TodoList.Domain.Enums;
using TodoList.Domain.Validation;

namespace TodoList.Domain.Entities
{
    public class User
    {
        public User(int id, string userName, string password, bool isActive, RoleUser role, string? email)
        {
            
            Id = id;
            IsActive = isActive;
            Role = role;
           

            ValidationAddDomainUser(userName, password, email);    
        }
      


        public int Id { get; private set; }

        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public RoleUser Role { get; private set; }
        public bool IsActive { get; private set; }



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


        public void UpdatePasswordUser(string newPassword)
        {
            ValidationPassword(newPassword);
            Password = newPassword;
        }

        public void UpdateUser(string userName, string email, string password, bool isActive)
        {
            if (string.IsNullOrEmpty(userName)) { }
            else
                UserName = userName;


            if (string.IsNullOrEmpty(email)) { }

            else
            {
                DomainExceptionValidation.When(EmailIsValid(email) is false, "O email não está em um formato válido");
                Email = email;
            }


            if (string.IsNullOrEmpty(password)) { }
               
            else
                ValidationPassword(password);

            if (string.IsNullOrEmpty(isActive.ToString())) { }


            else
                IsActive = isActive;
          
        }

        public void ValidationAddDomainUser(string userName, string password, string email)
        {
           DomainExceptionValidation.When(string.IsNullOrEmpty(userName), "O campo Name é de preenchimento obrigatório");
            DomainExceptionValidation.When(userName.Length <= 3, "O nome deve conter no mínimo 3 caracteres");
            DomainExceptionValidation.When(userName.Length >= 80, "O nome de conter no maxímo 80 caracteres");

            // Validação de senha.
            ValidationPassword(password);

            DomainExceptionValidation.When(EmailIsValid(email) is false, "O email não está em um formato válido");
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "O email é de preenchimento obrigatório");


            UserName = userName;
            Password = password;
            Email = email;


        }

        public void ValidationPassword(string password)
        {
            // Validação de senha.
            string regexPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$!%^&*])[A-Za-z\d@#$!%^&*]{8,}$";
            bool isPassaWordValid = Regex.IsMatch(password, regexPattern);

            DomainExceptionValidation.When(string.IsNullOrEmpty(password), "O campo senha é de preenchimento obrigatório");

            DomainExceptionValidation.When(!isPassaWordValid,
                "A senha deve conter pelo menos 1 letra maiúscula, 1 letra minúscula, 1 caractere especial e 1 número.");

            DomainExceptionValidation.When(password.Length <= 8, "A senha dever contér no mínimo 8 caracteres");
            DomainExceptionValidation.When(password.Length >= 80, "A senha de conter no maxímo 80 caracteres");

            Password = password;

        }
    }
}
