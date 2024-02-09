using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPadPw.Classes;
using DataAccess.AzureStorage;

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
            ExecuteTableManager tableManager = new ExecuteTableManager("User", DBConnect.NoSqlConnection);
            User u = tableManager.RetrieveEntity<User>("Email eq '" + user.Email + "'" +
                " and IsActive eq true").FirstOrDefault();
            if(u != null)
            {
                if(u.Password == user.Password)
                {
                    Session.Add("SessionValue", u);
                    Session.Timeout = 60;
                    return RedirectToAction("Index", "Notes");
                }
            }
            TempData["Msg"] = "Either Email or Password is wrong!!";
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
