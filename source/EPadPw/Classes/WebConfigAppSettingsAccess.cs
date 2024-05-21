using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace EPadPw.Logic
{
    public sealed class WebConfigAppSettingsAccess
    {
        public static string UserFullName { get { if (WebConfigurationManager.AppSettings["UserFullName"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["UserFullName"]?.ToString())) { return WebConfigurationManager.AppSettings["UserFullName"]?.ToString(); } return null; } }
        public static string UserEmail { get { if (WebConfigurationManager.AppSettings["UserEmail"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["UserEmail"]?.ToString())) { return WebConfigurationManager.AppSettings["UserEmail"]?.ToString(); } return null; } }
        public static string Password { get { if (WebConfigurationManager.AppSettings["Password"] != null && !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["Password"]?.ToString())) { return WebConfigurationManager.AppSettings["Password"]?.ToString(); } return null; } }
    }
}