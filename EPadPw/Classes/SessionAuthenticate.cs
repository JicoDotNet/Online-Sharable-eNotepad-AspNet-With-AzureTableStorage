using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace EPadPw
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class SessionAuthenticate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RouteValueDictionary LogoutRouteObj =
                    new RouteValueDictionary
                    {
                        { "action", "Index" },
                        { "controller", "Account" },
                        { "returnUrl", filterContext.HttpContext.Request.RawUrl }
                    };
            try
            {
                // If session exists
                if (filterContext.HttpContext.Session != null)
                {
                    //Login Session Check
                    if (filterContext.HttpContext.Session["SessionValue"] != null)
                    {
                        base.OnActionExecuting(filterContext);
                    }
                    else
                    {
                        filterContext.Result =
                            new RedirectToRouteResult(LogoutRouteObj);
                        return;
                    }
                }
                else
                {
                    //otherwise continue with action
                    base.OnActionExecuting(filterContext);
                }
            }
            catch (Exception e)
            {
                LogoutRouteObj.Add("Ex", e);
                filterContext.Result =
                            new RedirectToRouteResult(LogoutRouteObj);
                return;
            }
        }
    }
}