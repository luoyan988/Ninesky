using Ninesky.Areas.Admin.Extensions;
////////////////////
//版本V1
//创建日期2013-8-3
//修改日期：2013-12-02
//////////////////
using System.Web.Mvc;

namespace Ninesky.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }

        #region 分部视图
        /// <summary>
        /// 顶部视图
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public PartialViewResult Header()
        {
            return PartialView(AdministratorController.AdminInfo);
        }
        #endregion
    }
}
