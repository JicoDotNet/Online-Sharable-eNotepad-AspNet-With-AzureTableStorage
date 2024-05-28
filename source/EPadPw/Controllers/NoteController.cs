using DataAccess.AzureStorage;
using Newtonsoft.Json;
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
    public class NoteController : BaseController
    {
        [HttpPost]
        public ActionResult Start(string Note)
        {
            if (Note.Contains('/'))
            {
                string[] Urls = Note.Split('/');
                if (Urls.Length > 0)
                {
                    Note = Urls[Urls.Length - 1];
                }                
            }
            return RedirectToAction("Pad", "Note", new { id = Note });
        }

        public ActionResult Pad(string id)
        {
            ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
            Notepad notepad = tableManager.RetrieveEntity<Notepad>("RowKey eq '" + id + "' or NoteUri eq '" + id + "'").FirstOrDefault();
            if (notepad != null && notepad.IsActive)
            {
                notepad._User = new User
                {
                    Email = WebConfigAppSettingsAccess.UserEmail,
                    FullName = WebConfigAppSettingsAccess.UserFullName,
                    RowKey = WebConfigAppSettingsAccess.UserEmail,
                    IsActive = true
                };

                notepad.Note = string.Empty;
                notepad._Files = new List<NoteFile>();

                return View(notepad);
            }
            return View((object)null);
        }

        [HttpGet]
        [SessionAuthenticate]
        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
                string _query = "NoteUri eq '" + id + "' or RowKey eq '" + id + "'";
                List<Notepad> notepads = tableManager.RetrieveEntity<Notepad>(_query);
                if(notepads.Count() == 1)
                {
                    Notepad notepad = notepads.FirstOrDefault();

                    if (notepad != null
                    && notepad.PartitionKey == Credential.RowKey
                    && notepad.IsActive)
                    {
                        notepad.Note = Uri.UnescapeDataString(ReadFile(notepad.NotePath));
                        notepad._Files = JsonConvert.DeserializeObject<List<NoteFile>>(ReadFile(notepad.FilesPath));
                        return View(notepad);
                    }
                }
                
            }
            return View("_ViewNoNoteFound");
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(Notepad notepad)
        {
            if (!string.IsNullOrEmpty(notepad.Note))
            {
                UploadNote(notepad.Note, notepad.RowKey);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Availability(string id)
        {
            return Json(CheckAvailability(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UriChange(Notepad notepad)
        {
            if (CheckAvailability(notepad.NoteUri))
            {
                ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
                Notepad exsistingNotepad = tableManager.RetrieveEntity<Notepad>("RowKey eq '" + notepad.RowKey + "'").FirstOrDefault();
                if (exsistingNotepad != null)
                {
                    exsistingNotepad.NoteUri = notepad.NoteUri;
                    tableManager.UpdateEntity(exsistingNotepad);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionAuthenticate]
        public ActionResult File(NoteFile noteFile, string PartitionKey, string RowKey)
        {
            try
            {
                ExecuteBlobManager blobManager = new ExecuteBlobManager(PartitionKey, DBConnect.NoSqlConnection);
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase fileBase = Request.Files["FilePath"];
                    string[] path = new string[] { RowKey, "files" };
                    noteFile.FilePath = blobManager.UploadFile(fileBase, path);
                    noteFile.FileName = Request.Files["FilePath"].FileName;
                }                

                string JsonText = string.Empty;
                List<NoteFile> filesAgger = JsonConvert.DeserializeObject<List<NoteFile>>(ReadFile("/File/" + RowKey + ".json"));
                filesAgger.Add(noteFile);
                JsonText = JsonConvert.SerializeObject(filesAgger);

                UploadFile(JsonText, RowKey);

                return RedirectToAction("Edit", new { id = RowKey });
            }
            catch
            {
                return RedirectToAction("Index", "Notes");
            }
        }

        [SessionAuthenticate]
        public ActionResult DeleteFile(string path, string PartitionKey, string RowKey)
        {
            try
            {
                ExecuteBlobManager blobManager = new ExecuteBlobManager(PartitionKey, DBConnect.NoSqlConnection);

                string JsonText = string.Empty;
                List<NoteFile> filesAgger = JsonConvert.DeserializeObject<List<NoteFile>>(ReadFile("/File/" + RowKey + ".json"));
                NoteFile noteFile = filesAgger.FirstOrDefault(a => a.FilePath == path);

                blobManager.DeleteBlob(path);
                filesAgger.Remove(noteFile);
                JsonText = JsonConvert.SerializeObject(filesAgger);

                UploadFile(JsonText, RowKey);

                return RedirectToAction("Edit", new { id = RowKey });
            }
            catch
            {
                return RedirectToAction("Index", "Notes");
            }
        }

        private string UploadFile(string val, string RowKey)
        {
            try
            {
                string message = val;

                string path = string.Empty;
                path = Server.MapPath("~/File/");
                path = path + RowKey + ".json";

                System.IO.File.WriteAllText(path, message);

                return "/File/" + RowKey + ".json";
            }
            catch (Exception ex)
            {
                return "/File/" + RowKey + ".json";
            }
        }

        private string ReadFile(string VirtualPath)
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

        private string UploadNote(string val, string RowKey)
        {
            try
            {
                string message = val;

                string path = string.Empty;
                path = Server.MapPath("~/Note/");
                path = path + RowKey + ".txt";

                System.IO.File.WriteAllText(path, message);

                //using (StreamWriter writer = new StreamWriter(path, true))
                //{
                //    writer.WriteLine(message);
                //    writer.Close();
                //}

                return "/Note/" + RowKey + ".txt";
            }
            catch (Exception ex)
            {
                return "/Note/" + RowKey + ".txt";
            }
        }

        private bool CheckAvailability(string id)
        {
            try
            {
                if (ValidInputForNoteUri(id))
                {

                    ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
                    Notepad notepad = tableManager.RetrieveEntity<Notepad>("NoteUri eq '" + id + "' or RowKey eq '" + id + "'").FirstOrDefault();
                    if (notepad == null)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidInputForNoteUri(string noteUri)
        {
            if (noteUri is null)
            {
                return false;
            }

            if (noteUri.Length < 8 || noteUri.Length > 32)
                return false;

            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";

            foreach (char c in noteUri)
            {
                if (!validChars.Contains(c))
                    return false;
            }

            return true;
        }
    }
}