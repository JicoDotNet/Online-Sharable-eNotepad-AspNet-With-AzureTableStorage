using DataAccess.AzureStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPadPw.Classes;
using EPadPw.Models;

namespace EPadPw.Controllers
{
    public class NotesController : Controller
    {
        [SessionAuthenticate]
        public ActionResult Index()
        {
            User user = (User)Session["SessionValue"];
            ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
            List<Notepad> notepads = tableManager.RetrieveEntity<Notepad>("PartitionKey eq '" + user.RowKey + "' and IsActive eq true");
            return View(notepads);
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult Index(Notepad notepad)
        {
            if (!string.IsNullOrEmpty(notepad.Subject))
            {
                User user = (User)Session["SessionValue"];

                notepad.PartitionKey = user.RowKey;
                notepad.UserId = user.RowKey;
                notepad.RowKey = GenericLogic.TimeStamp(GenericLogic.IstNow).ToString("X");

                notepad.NotePath = UploadNote("write your note here", notepad.RowKey);
                notepad.FilesPath = UploadFile("[]", notepad.RowKey);

                ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
                notepad.IsActive = true;
                tableManager.InsertEntity(notepad, false);
                return RedirectToAction("Edit", "Note", new { id = notepad.RowKey });
            }
            return RedirectToAction("Index");
        }

        [SessionAuthenticate]
        public ActionResult Delete(string id)
        {
            User user = (User)Session["SessionValue"];

            ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
            Notepad notepad = tableManager.RetrieveEntity<Notepad>("PartitionKey eq '" + user.RowKey
                    + "' and IsActive eq true and RowKey eq '" + id + "'").FirstOrDefault();
            notepad.IsActive = false;
            tableManager.InsertEntity(notepad, false);
            return RedirectToAction("Index");
        }

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
            catch (Exception ex)
            {
                return "/Note/" + RowKey + ".txt";
            }
        }

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
            catch (Exception ex)
            {
                return "/File/" + RowKey + ".json";
            }
        }

        public string ReadFile(string VirtualPath)
        {
            //VirtualPath = "http://localhost:49312/" + "~/" + VirtualPath;
            try
            {
                return System.IO.File.ReadAllText(Server.MapPath("~/" + VirtualPath));
            }
            catch (Exception ex)
            {
                return "[]";
            }
        }
    }
}