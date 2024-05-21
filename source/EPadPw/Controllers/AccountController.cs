using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPadPw.Logic;
using DataAccess.AzureStorage;
using EPadPw.Models;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace EPadPw.Controllers
{
    public class AccountController : BaseController
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
                user.RowKey = Regex.Replace(WebConfigAppSettingsAccess.UserEmail, @"[^0-9a-zA-Z]", "");

                Response.Cookies.Add(new HttpCookie("Credential")
                {
                    Value = JsonConvert.SerializeObject(user),
                    Expires = DateTime.Now.AddMonths(11)
                });
                return RedirectToAction("Index", "Notes");
            }            
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult PartialUserBind()
        {
            return PartialView("_PartialUserBindPage", Credential);
        }
    }
}
