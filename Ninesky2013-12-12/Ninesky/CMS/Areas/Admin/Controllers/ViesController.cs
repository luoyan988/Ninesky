using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninesky.Models;
using Ninesky.Areas.Admin.Extensions;

namespace Ninesky.Areas.Admin.Controllers
{
    /// <summary>
    /// 视图控制器
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.11.23
    /// </remarks>
    /// </summary>
    [AdminAuthorize]
    public class ViewController : Controller
    {
        /// <summary>
        /// 控制器视图列表
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <returns>视图列表<br />
        /// ParameterViewModel的json格式<br />
        /// Name为视图文件名，不包含后缀<br />
        /// Value为自根目录起的完整路径。如:“~/Views/Category/Index.cshtml”。
        /// </returns>
        public JsonResult Views(string controllerName)
        {
            string _parth = Server.MapPath("~/Views/" + controllerName);
            System.IO.DirectoryInfo _dirInfo = new System.IO.DirectoryInfo(_parth);
            if (_dirInfo == null) return null;
            var _files = _dirInfo.GetFiles().Select(f => new ParameterViewModel { Name = f.Name.Remove(f.Name.LastIndexOf(".")), Value = "~/Views/" + controllerName + f.Name });
            return Json(_files);
        }

    }
}
