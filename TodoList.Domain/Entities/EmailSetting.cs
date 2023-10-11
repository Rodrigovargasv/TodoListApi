
namespace TodoList.Domain.Entities
{
    public class EmailSetting
    {
        public string SmtpServer { get; }
        public int Port { get; }
        public string Password { get; }
        public string SenderEmail { get; }
    }
}
