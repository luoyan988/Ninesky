using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninesky.Repository;

namespace Ninesky.Controllers
{
    public class ItemController : Controller
    {
        private CommonModelRepository cModelRsy;
        public ItemController()
        {
            cModelRsy = new CommonModelRepository();
        }

        #region 公共部分
        public ActionResult Index(int id)
        {
            return View();
        }
        #endregion
    }
}
