using System.Web.Mvc;

namespace Ninesky.Areas.Admin
{
    public class ControlAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Ninesky.Areas.Admin.Controllers" }
            );
        }
    }
}
