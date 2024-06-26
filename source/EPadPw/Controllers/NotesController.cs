﻿using DataAccess.AzureStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPadPw.Logic;
using EPadPw.Models;

namespace EPadPw.Controllers
{
    public class NotesController : BaseController
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
            List<Notepad> notepads = tableManager.RetrieveEntity<Notepad>("PartitionKey eq '" + Credential.RowKey + "' and IsActive eq true");
            return View(notepads);
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult Index(Notepad notepad)
        {
            if (!string.IsNullOrEmpty(notepad.Subject))
            {
                notepad.PartitionKey = Credential.RowKey;
                notepad.UserId = Credential.RowKey;
                notepad.RowKey = GenericLogic.TimeStamp(GenericLogic.IstNow).ToString("X");
                notepad.NoteUri = notepad.RowKey;
                notepad.Subject = notepad.Subject.Trim();

                notepad.NotePath = UploadNote("write your note here", notepad.RowKey);
                notepad.FilesPath = UploadFile("[]", notepad.RowKey);

                ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
                notepad.IsActive = true;
                tableManager.InsertEntity(notepad, false);
                return RedirectToAction("Edit", "Note", new { id = notepad.NoteUri });
            }
            return RedirectToAction("Index");
        }

        [SessionAuthenticate]
        public ActionResult Delete(string id)
        {
            ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
            Notepad notepad = tableManager.RetrieveEntity<Notepad>("PartitionKey eq '" + Credential.RowKey
                    + "' and IsActive eq true and RowKey eq '" + id + "'").FirstOrDefault();
            notepad.IsActive = false;
            tableManager.InsertEntity(notepad, false);
            return RedirectToAction("Index");
        }

        [SessionAuthenticate]
        public string UploadNote(string val, string RowKey)
        {
            try
            {
                string message = val;

                string path = string.Empty;
                path = Server.MapPath("~/Note/");
                path = path + RowKey + ".txt";

                System.IO.File.WriteAllText(path, string.Empty);

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }

                return "/Note/" + RowKey + ".txt";
            }
            catch
            {
                return "/Note/" + RowKey + ".txt";
            }
        }

        [SessionAuthenticate]
        public string UploadFile(string val, string RowKey)
        {
            try
            {
                string message = val;

                string path = string.Empty;
                path = Server.MapPath("~/File/");
                path = path + RowKey + ".json";

                System.IO.File.WriteAllText(path, string.Empty);

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }

                return "/File/" + RowKey + ".json";
            }
            catch
            {
                return "/File/" + RowKey + ".json";
            }
        }

        [SessionAuthenticate]
        public string ReadFile(string VirtualPath)
        {
            //VirtualPath = "http://localhost:49312/" + "~/" + VirtualPath;
            try
            {
                return System.IO.File.ReadAllText(Server.MapPath("~/" + VirtualPath));
            }
            catch
            {
                return "[]";
            }
        }
    }
}