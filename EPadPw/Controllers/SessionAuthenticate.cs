using EPadPw.Models;
using Newtonsoft.Json;
using System.Web.Routing;

namespace System.Web.Mvc
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
                HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies["Credential"];
                if (cookie != null)
                {
                    try
                    {
                        JsonConvert.DeserializeObject<User>(cookie.Value);
                        base.OnActionExecuting(filterContext);
                        return;
                    }
                    catch
                    {
                        filterContext.Result =
                        new RedirectToRouteResult(LogoutRouteObj);
                        return;
                    }
                }
                filterContext.Result = new RedirectToRouteResult(LogoutRouteObj);
                return;
            }
            catch (Exception e)
            {
                LogoutRouteObj.Add("Ex", e);
                filterContext.Result = new RedirectToRouteResult(LogoutRouteObj);
                return;
            }
        }
    }
}