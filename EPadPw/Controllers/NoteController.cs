using DataAccess.AzureStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPadPw.Classes;

namespace EPadPw.Controllers
{
    public class NoteController : Controller
    {
        [HttpPost]
        public ActionResult Start(string Note)
        {
            return RedirectToAction("Pad", "Note", new { id = Note });
        }

        public ActionResult Pad(string id)
        {
            ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
            Notepad notepad = tableManager.RetrieveEntity<Notepad>("IsActive eq true and RowKey eq '" + id + "'").FirstOrDefault();
            if (notepad != null)
            {
                tableManager = new ExecuteTableManager("User", DBConnect.NoSqlConnection);
                notepad._User = tableManager.RetrieveEntity<User>("RowKey eq '" + notepad.PartitionKey + "'").FirstOrDefault();

                notepad.Note = string.Empty;
                notepad._Files = new List<NoteFile>();

                return View(notepad);
            }
            return View((object)null);
        }

        [SessionAuthenticate]
        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                User user = (User)Session["SessionValue"];

                ExecuteTableManager tableManager = new ExecuteTableManager("notepad", DBConnect.NoSqlConnection);
                Notepad notepad = tableManager.RetrieveEntity<Notepad>("PartitionKey eq '" + user.RowKey
                    + "' and IsActive eq true and RowKey eq '" + id + "'").FirstOrDefault();
                if (notepad != null)
                {
                    notepad.Note = Uri.UnescapeDataString(ReadFile(notepad.NotePath));
                    notepad._Files = JsonConvert.DeserializeObject<List<NoteFile>>(ReadFile(notepad.FilesPath));
                    return View(notepad);
                }
            }
            return RedirectToAction("Index", "Notes");
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Notes");
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

                System.IO.File.WriteAllText(path, message);

                //using (StreamWriter writer = new StreamWriter(path, true))
                //{
                //    writer.WriteLine(message);
                //    writer.Close();
                //}

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

        public string UploadNote(string val, string RowKey)
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
    }
}