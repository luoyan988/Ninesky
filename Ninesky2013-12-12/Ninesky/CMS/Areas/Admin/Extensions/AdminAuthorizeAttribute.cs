using Ninesky.Areas.Admin.Controllers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninesky.Areas.Admin.Extensions
{
    /// <summary>
    ///  管理员权限验证
    /// </summary>
    public class AdminAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (string.IsNullOrEmpty(AdministratorController.AdminName)) return false;
            else return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult() { Data = new Ninesky.Models.JsonViewModel() { Authentication = 1, Success = false, Message = "用户未登录或登录已超时。" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                //filterContext.Result = new JsonResult() { Data = new Ninesky.Models.JsonViewModel() { Authentication = 1, Success = false, Message = "用户未登录或登录超时。" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                filterContext.Result = new RedirectToRouteResult("Admin_default", new RouteValueDictionary(new { controller = "Administrator", action = "Login" }));
            }
        }
    }
}