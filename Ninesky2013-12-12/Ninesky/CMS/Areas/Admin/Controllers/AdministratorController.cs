/////////////////
//管理员后台管理
//版本v1
//创建日期2013-7-25
//最后修改2013-11-11
/////////////////


using Ninesky.Areas.Admin.Extensions;
using Ninesky.Areas.Admin.Models;
using Ninesky.Areas.Admin.Repository;
using Ninesky.Models;
using System.Linq;
using System.Web.Mvc;

namespace Ninesky.Areas.Admin.Controllers
{
    public class AdministratorController : Controller
    {
        private InterfaceAdministrator adminRsy;

        #region 静态属性
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public static string AdminName
        {
            get
            {
                string _adminName = string.Empty;
                if (System.Web.HttpContext.Current.Session["AdminName"] != null) _adminName = System.Web.HttpContext.Current.Session["AdminName"].ToString();
                return _adminName;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) System.Web.HttpContext.Current.Session.Remove("AdminName");
                else
                {
                    System.Web.HttpContext.Current.Session.Timeout = 60;
                    System.Web.HttpContext.Current.Session.Add("AdminName", value);
                }
            }
        }
        /// <summary>
        /// 管理员信息
        /// </summary>
        public static Administrator AdminInfo
        {
            get
            {
                AdministratorRepository _adminRsy = new AdministratorRepository();
                return _adminRsy.Find(AdministratorController.AdminName);
            }
        }
        #endregion

        public AdministratorController()
        {
            adminRsy = new AdministratorRepository();
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public PartialViewResult Add()
        {
            return PartialView();
        }

        [AdminAuthorize]
        [HttpPost]
        public JsonResult Add(Administrator admin)
        {
            JsonViewModel _jdata = new JsonViewModel();
            if (ModelState.IsValid)
            {
                if (adminRsy.Find(admin.AdminName) != null)
                {
                    _jdata.Success = false;
                    _jdata.Message = "管理员名称已存在！";
                }
                else
                {
                    admin.IsPreset = false;
                    if (adminRsy.Add(admin))
                    {
                        _jdata.Success = true;
                        _jdata.Message = "保存成功√！";
                    }
                    else
                    {
                        _jdata.Success = false;
                        _jdata.Message = "数据未能保存到数据库！";
                    }
                }
            }
            else
            {
                var _eItem = ModelState.Where(m => m.Value.Errors.Count > 0);
                foreach (var i in _eItem)
                {
                    _jdata.ValidationList.Add(i.Key, "验证失败！");
                }
            }
            return Json(_jdata);
        }

        /// <summary>
        /// 管理员主页面
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public PartialViewResult Index()
        {
            return PartialView(); ;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public PartialViewResult ChangePassword()
        {
            return PartialView();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPwd">原密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        [AdminAuthorize]
        [HttpPost]
        public JsonResult ChangePassword(string oldPwd, string newPwd)
        {
            JsonViewModel _jdata = new JsonViewModel();
            if (ModelState.IsValid)
            {
                var _admin = AdministratorController.AdminInfo;
                if (_admin == null)
                {
                    _jdata.Success = false;
                    _jdata.Message = "登录已超时，请重新登录！";
                }
                else if (Common.Sha256(oldPwd) != _admin.PassWord)
                {
                    _jdata.Success = false;
                    _jdata.Message = "原密码错误！";
                }
                else
                {
                    _admin.PassWord = Common.Sha256(newPwd);
                    if (adminRsy.Modify(_admin))
                    {
                        _jdata.Success = true;
                        _jdata.Message = "保存成功√！";
                    }
                    else
                    {
                        _jdata.Success = false;
                        _jdata.Message = "数据未能保存到数据库！";
                    }
                }
            }
            else
            {
                var _eItem = ModelState.Where(m => m.Value.Errors.Count > 0);
                foreach (var i in _eItem)
                {
                    _jdata.ValidationList.Add(i.Key, "验证失败！");
                }
            }
            return Json(_jdata);
        }
        
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="Id">管理员Id</param>
        /// <returns></returns>
        [AdminAuthorize]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            JsonViewModel _jdata = new JsonViewModel();
            var _admin = adminRsy.Find(Id);
            if (_admin == null)
            {
                _jdata.Success = false;
                _jdata.Message = "Id为: " + Id + " 的管理员不存在!";
            }
            else if (_admin.IsPreset)
            {
                _jdata.Success = false;
                _jdata.Message = "不能删除系统预置管理员账号！";
            }
            else
            {
                if (adminRsy.Delete(Id))
                {
                    _jdata.Success = true;
                    _jdata.Message = "删除成功√";
                }
                else
                {
                    _jdata.Success = false;
                    _jdata.Message = "删除失败！";
                }
            }
            return Json(_jdata);

        }
        
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public JsonResult List()
        {
            return Json(adminRsy.Find());
        }
       
        /// <summary>
        /// 登录
        /// </summary>
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult Login(string adminName, string passWord)
        {
            JsonViewModel _jdata = new JsonViewModel();
            adminName = Server.HtmlEncode(adminName);
            passWord = Ninesky.Common.Sha256(Server.HtmlEncode(passWord));
            int _code = adminRsy.Authentication(adminName, passWord);
            if (_code == 1)
            {
                AdministratorController.AdminName = adminName;
                _jdata.Success = true;
                _jdata.Message = "登录成功！";
            }
            else if (_code == 0)
            {
                _jdata.Success = false;
                _jdata.Message = "密码错误！";
            }
            else if (_code == -1)
            {
                _jdata.Success = false;
                _jdata.Message = "管理员账号不存在！";
            }
            else
            {
                _jdata.Success = false;
                _jdata.Message = "发生未知错误，请刷新后重新登录！";
            }
            return Json(_jdata);
        }

        /// <summary>
        /// 用户是否登录
        /// </summary>
        /// <returns>Json类型。true表示已登录，false表示未登录</returns>
        public JsonResult Logined()
        {
            if (string.IsNullOrEmpty(AdministratorController.AdminName)) return Json(false);
            else return Json(true);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            AdministratorController.AdminName = string.Empty;
            return RedirectToAction("Login", "Administrator");
        }

    }

}
