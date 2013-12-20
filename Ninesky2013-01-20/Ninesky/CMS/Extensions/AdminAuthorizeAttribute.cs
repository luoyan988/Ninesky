using System;

namespace System.Web.Mvc
{
    /// <summary>
    ///  管理员权限验证
    /// </summary>
    public class AdminAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }
    }
}