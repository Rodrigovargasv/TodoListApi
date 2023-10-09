
namespace TodoList.Domain.Entities
{
    public class EmailSetting
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }
    }
}
