using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace EPadPw.Classes
{
    public class DBConnect
    {
        public static object NoSqlConnection { get; } = WebConfigurationManager.AppSettings["NoSqlConnection"].ToString();
    }
}