

using Newtonsoft.Json;
using System.Text.RegularExpressions;
using TodoList.Domain.Validation;

namespace TodoList.Domain.Entities
{
    public class User
    {
        public User(int id, string userName, string password, bool isActive)
        {
            
            Id = id;
            IsActive = isActive;

            ValidationAddDomainUser(userName, password);    
        }

        public int Id { get; private set; }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }


        public void ValidationAddDomainUser(string userName, string password)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(userName), "O campo Name é de preenchimento obrigatório");

            // Validação de senha.
            string regexPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$!%^&*])[A-Za-z\d@#$!%^&*]{8,}$";
            bool isPassaWordValid = Regex.IsMatch(password, regexPattern);

            DomainExceptionValidation.When(string.IsNullOrEmpty(password), "O campo PassWord é de preenchimento obrigatório");

            DomainExceptionValidation.When(!isPassaWordValid, 
                "A senha deve conter pelo menos 1 letra maiúscula, 1 letra minúscula, 1 caractere especial e 1 número.");
            
            DomainExceptionValidation.When(password.Length <= 8, "A senha dever contér no mínimo 8 caracteres");


            UserName = userName;
            Password = password;


        }
        

    }
}
