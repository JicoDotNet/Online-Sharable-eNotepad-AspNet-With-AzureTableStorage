using EPadPw.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace EPadPw.Models
{
    public class Notepad : TableEntity
    {
        public string NotePath { get; set; }
        public bool IsActive { get; set; }
        public string Subject { get; set; }
        public string UserId { get; set; }

        public string Note { get; set; }
        public string FilesPath { get; set; }
        public List<NoteFile> _Files { get; set; }

        public User _User { get; set; }
    }
}