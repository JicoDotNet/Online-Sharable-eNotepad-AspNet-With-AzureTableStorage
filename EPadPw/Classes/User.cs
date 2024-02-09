using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPadPw.Classes
{
    public class User : TableEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Details { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
