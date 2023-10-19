
using TodoList.Domain.Enums;

namespace TodoList.Application.DTOs
{
    public class UserDto
    {

        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
