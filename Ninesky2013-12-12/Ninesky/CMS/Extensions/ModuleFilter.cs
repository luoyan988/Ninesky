using Ninesky.Repository;
using System.Web.Mvc;

namespace Ninesky.Extensions
{
    /// <summary>
    /// 模块过滤器
    /// 屏蔽禁用模块
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.12.12
    /// </remarks>
    /// </summary>
    public class ModuleFilter:ActionFilterAttribute
    {
        /// <summary>
        /// 模型名
        /// </summary>
        public string Model { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(Model)) filterContext.Result = new ContentResult() { Content = "未指定模块" };
            switch (Model)
            {
                case "Article":
                    InterfaceModule _moduleRepository = new ModuleRepository();
                    var _module = _moduleRepository.Find(Model);
                    if (_module == null) filterContext.Result = new ContentResult() { Content = "未找到指定模块" };
                    else if (!_module.Enable) filterContext.Result = new ContentResult() { Content = _module.Name + "已关闭" };
                    break;
                default:
                    filterContext.Result = new ContentResult() { Content = "未找到指定模块" };
                    break;
            }
        }
    }
}