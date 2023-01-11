using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPadPw.Classes
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

    public class NoteFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileDetails { get; set; }
    }
}