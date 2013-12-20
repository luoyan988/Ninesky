using Ninesky.Areas.Admin.Extensions;
using Ninesky.Areas.Admin.Repository;
using Ninesky.Models;
using System.Web.Mvc;

namespace Ninesky.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台模块控制器
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.11.24
    /// 修改2013.12.12
    /// </remarks>
    /// </summary>
    [AdminAuthorize]
    public class ModuleController : Controller
    {
        InterfaceModule moduleRepository = new ModuleRepository();   
        /// <summary>
        /// 模块主页
        /// </summary>
        /// <returns>视图</returns>
        public ActionResult Center()
        {
            return PartialView();
        }

        /// <summary>
        /// 模块信息页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return PartialView(moduleRepository.Find(id));
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="enable">启用状态。【0-全部，1-启用，2-禁用】</param>
        /// <returns>
        /// 普通请求时返回包含强类型List\<Module\>的视图<br />
        /// ajax请求返回json类型List\<Module\>
        /// </returns>
        public ActionResult Items(int id = 0)
        {

            var _modules = moduleRepository.Find();
            if (Request.IsAjaxRequest()) return Json(_modules);
            return View(_modules);
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return PartialView(moduleRepository.Find());
        }

        /// <summary>
        /// 修改模块信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify(int? ModuleId,bool? Enable,string Description)
        {
            JsonViewModel _jsonViewModel = new JsonViewModel() { Authentication = 0, Success = true };
            bool _modifyed = false;
            if (ModuleId == null) 
            {
                _jsonViewModel.Success = false;
                _jsonViewModel.ValidationList.Add("ModuleId", "模块Id不能为空");
            }
            if (Enable == null)
            {
                _jsonViewModel.Success = false;
                _jsonViewModel.ValidationList.Add("Enable", "启用状态不能为空");
            }
            if (_jsonViewModel.Success)
            {
                var _module = moduleRepository.Find((int)ModuleId);
                if (_module == null)
                {
                    _jsonViewModel.Success = false;
                    _jsonViewModel.Message = "模块存在";
                }
                else
                {
                    if (_module.Enable != Enable) { _module.Enable = (bool)Enable; _modifyed = true; }
                    if (_module.Description != Description) { _module.Description = Description; _modifyed = true; }
                    if (_modifyed)
                    {
                        if (moduleRepository.Modify(_module)) { _jsonViewModel.Success = true; _jsonViewModel.Message = "修改模块成功。"; }
                        else { _jsonViewModel.Success = false; _jsonViewModel.Message = "数据未能保存到数据库"; }
                    }
                    else { _jsonViewModel.Success = false; _jsonViewModel.Message = "数据未发生更改"; }
                }
            }
            else _jsonViewModel.Message = "数据验证失败！";
            return Json(_jsonViewModel);
        }
    }
}
