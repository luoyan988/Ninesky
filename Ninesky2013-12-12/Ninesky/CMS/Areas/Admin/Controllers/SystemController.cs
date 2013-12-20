using Ninesky.Areas.Admin.Extensions;
using Ninesky.Models;
using Ninesky.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Ninesky.Areas.Admin.Controllers
{
    /// <summary>
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.8.5
    /// 修改2013.11.3
    /// </remarks>
    /// </summary>
    [AdminAuthorize]
    public class SystemController : Controller
    {
        #region 分部视图
        /// <summary>
        /// 基本信息设置
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Config()
        {
            var _siteConfig = new SiteConfigRepository().Find();
            if (_siteConfig == null) _siteConfig = new SiteConfig() { Id = 0, Name = "NineSky", Title = "欢迎光临NineSky！" };
            return PartialView(_siteConfig);
        }

        /// <summary>
        /// Config保存
        /// </summary>
        /// <param name="siteConfig"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Config(SiteConfig siteConfig)
        {
            JsonViewModel _jdata = new JsonViewModel();
            if (ModelState.IsValid)
            {
                var _scRsy = new SiteConfigRepository();
                if (_scRsy.Save(siteConfig))
                {
                    _jdata.Success = true;
                    _jdata.Message = ("保存成功√");
                }
                else
                {
                    _jdata.Success = false;
                    _jdata.Message = ("保存数据时发生错误");
                }
            }
            else
            {
                _jdata.Success = false;
                var _eItem = ModelState.Where(m => m.Value.Errors.Count > 0);
                foreach (var i in _eItem)
                {
                    _jdata.ValidationList.Add(i.Key, "验证失败！");
                }
                _jdata.Message = ("保存数据时发生错误");

            }
            return Json(_jdata);
        }

        /// <summary>
        /// 左侧列表视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PartialWestMenu()
        {
            return PartialView();
        }
        #endregion
    }
}
