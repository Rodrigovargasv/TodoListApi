
using System.Net.Mail;
using System.Text.RegularExpressions;
using TodoList.Domain.Enums;
using TodoList.Domain.Validation;

namespace TodoList.Domain.Entities
{
    public class User
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleUser Role { get; set; }
        public bool IsActive { get; set; }

    }
}
