using Microsoft.WindowsAzure.Storage.Table;

namespace EPadPw.Models
{
    public class User : TableEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
