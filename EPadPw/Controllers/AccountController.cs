using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPadPw.Classes;
using DataAccess.AzureStorage;
using EPadPw.Models;
using System.Web.UI.WebControls;

namespace EPadPw.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            if (TempData["Msg"] != null)
            {
                ViewBag.Msg = TempData["Msg"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(User user)
        {
            if (WebConfigAppSettingsAccess.UserEmail != user.Email
                    || WebConfigAppSettingsAccess.Password != user.Password)
            {
                TempData["Msg"] = "Either Email or Password is wrong!!";
                return RedirectToAction("Index");
            }
            else
            {
                user.IsActive = true;
                user.FullName = WebConfigAppSettingsAccess.UserFullName;
                user.Password = string.Empty;
                user.RowKey = WebConfigAppSettingsAccess.UserEmail;

                Session.Add("SessionValue", user);
                Session.Timeout = 60;
                return RedirectToAction("Index", "Notes");
            }            
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
