using System;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using Ninesky.Models;
using Ninesky.Repository;

namespace Ninesky.Controllers
{
    public class UserController : Controller
    {
        private UserRepository userRsy;
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserRegister userReg)
        {
            if (Session["VerificationCode"] == null || Session["VerificationCode"].ToString() == "")
            {
                Error _e = new Error { Title = "验证码不存在", Details = "在用户注册时，服务器端的验证码为空，或向服务器提交的验证码为空", Cause = Server.UrlEncode("<li>你注册时在注册页面停留的时间过久页已经超时</li><li>您绕开客户端验证向服务器提交数据</li>"), Solution = Server.UrlEncode("返回<a href='" + Url.Action("Register", "User") + "'>注册</a>页面，刷新后重新注册") };
                return RedirectToAction("Error", "Prompt", _e);
            }
            else if (Session["VerificationCode"].ToString() != userReg.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "×");
                return View();
            }
            userRsy = new UserRepository();
            if (userRsy.Exists(userReg.UserName))
            {
                ModelState.AddModelError("UserName", "用户名已存在");
                return View();
            }
            User _user = userReg.GetUser();
            _user.RegTime = System.DateTime.Now;
            if (userRsy.Add(_user))
            {
                Notice _n = new Notice { Title = "注册成功", Details = "您已经成功注册，用户为：" + _user.UserName + " ，请牢记您的密码！", DwellTime = 5, NavigationName = "登陆页面", NavigationUrl = Url.Action("Login", "User") };
                return RedirectToAction("Notice", "Prompt", _n);
            }
            else
            {
                Error _e = new Error { Title = "注册失败", Details = "在用户注册时，发生了未知错误", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("Register", "User") + "'>注册</a>页面，输入正确的信息后重新注册</li><li>联系网站管理员</li>") };
                return RedirectToAction("Error", "Prompt", _e);
            }
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin login)
        {
            //验证验证码
            if (Session["VerificationCode"] == null || Session["VerificationCode"].ToString() == "")
            {
                Error _e = new Error { Title = "验证码不存在", Details = "在用户注册时，服务器端的验证码为空，或向服务器提交的验证码为空", Cause = Server.UrlEncode("<li>你注册时在注册页面停留的时间过久页已经超时</li><li>您绕开客户端验证向服务器提交数据</li>"), Solution = Server.UrlEncode("返回<a href='" + Url.Action("Register", "User") + "'>注册</a>页面，刷新后重新注册") };
                return RedirectToAction("Error", "Prompt", _e);
            }
            else if (Session["VerificationCode"].ToString() != login.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "×");
                return View();
            }
            //验证账号密码
            userRsy = new UserRepository();
            if (userRsy.Authentication(login.UserName, Common.Text.Sha256(login.Password)) == 0)
            {
                HttpCookie _cookie = new HttpCookie("User");
                _cookie.Values.Add("UserName", login.UserName);
                _cookie.Values.Add("Password", Common.Text.Sha256(login.Password));
                Response.Cookies.Add(_cookie);
                if (Request.QueryString["ReturnUrl"] != null) return Redirect(Request.QueryString["ReturnUrl"]);
                else return RedirectToAction("Default", "User");
            }
            else
            {
                ModelState.AddModelError("Message", "登陆失败！");
                return View();
            }

        }
        
        #region 用户中心
        [UserAuthorize]
        public ActionResult Default()
        {
            userRsy = new UserRepository();
            var _user = userRsy.Find(UserName);
            return View(_user);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [UserAuthorize]
        public ActionResult ChangePassword(UserChangePassword userChangePassword)
        {
            userRsy = new UserRepository();
            if (userRsy.Authentication(UserName, Common.Text.Sha256(userChangePassword.Password)) == 0)
            {
                var _user = userRsy.Find(UserName);
                if (_user == null)
                {
                    Error _e = new Error { Title = "修改密码失败", Details = "修改密码时，系统查询不到用户信息", Cause = Server.UrlEncode("<li>用户在修改密码界面停留的时间过长，登录信息已失效。</li><li>系统错误。</li>"), Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ChangePassword", "User") + "'>修改密码</a>页面，输入正确的信息后重新注册</li><li>联系网站管理员</li>") };
                    return RedirectToAction("Error", "Prompt", _e);
                }
                _user.Password = Common.Text.Sha256(userChangePassword.NewPassword);
                if (userRsy.Update(_user))
                {
                    Notice _n = new Notice { Title = "成功修改密码", Details = "您已经成功修改密码，请牢记您的新密码！", DwellTime = 5, NavigationName = "登陆页面", NavigationUrl = Url.Action("Login", "User") };
                    return RedirectToAction("Notice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "修改密码失败", Details = "修改密码时，更新数据库失败！", Cause = Server.UrlEncode("<li>系统错误。</li>"), Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ChangePassword", "User") + "'>修改密码</a>页面，输入正确的信息后重新注册</li><li>联系网站管理员</li>") };
                    return RedirectToAction("Error", "Prompt", _e);
                }
            }
            else
            {
                ModelState.AddModelError("Password", "原密码不正确，请重新输入");
                return View();
            }
            
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult ChangeInfo()
        {
            userRsy = new UserRepository();
            var _user = userRsy.Find(UserName);
            return View(_user);
        }
        [HttpPost]
        [UserAuthorize]
        public ActionResult ChangeInfo(User user)
        {
            userRsy = new UserRepository();
            if(userRsy.Authentication(UserName,Ninesky.Common.Text.Sha256(user.Password))==0)
            {
                var _user = userRsy.Find(UserName);
                _user.Gender = user.Gender;
                _user.Email = user.Email;
                _user.QQ = user.QQ;
                _user.Tel = user.Tel;
                _user.Address = user.Address;
                _user.PostCode = user.PostCode;
                if (userRsy.Update(_user))
                {
                    Notice _n = new Notice { Title = "修改资料成功", Details = "您已经成功修改资料！", DwellTime = 5, NavigationName = "用户首页", NavigationUrl = Url.Action("Default", "User") };
                    return RedirectToAction("UserNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "修改资料失败", Details = "在修改用户资料时时，更新的资料未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ChangeInfo", "User") + "'>修改资料</a>页面，输入正确的信息后重新操作</li><li>联系网站管理员</li>") };
                    return RedirectToAction("UserError", "Prompt", _e);
                }
            }
            else
            {
                ModelState.AddModelError("Password","密码错误！");
                return View();
            }
            
            
        }
        #endregion
        /// <summary>
        /// 绘制验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificationCode()
        {
            int _verificationLength = 6;
            int _width = 100, _height = 20;
            SizeF _verificationTextSize;
            Bitmap _bitmap = new Bitmap(Server.MapPath("~/Content/Images/Texture.jpg"), true);
            TextureBrush _brush = new TextureBrush(_bitmap);
            //获取验证码
            string _verificationText = Common.Text.VerificationText(_verificationLength);
            //存储验证码
            Session["VerificationCode"] = _verificationText.ToUpper();
            Font _font = new Font("Arial", 14, FontStyle.Bold);
            Bitmap _image = new Bitmap(_width, _height);
            Graphics _g = Graphics.FromImage(_image);
            //清空背景色
            _g.Clear(Color.White);
            //绘制验证码
            _verificationTextSize = _g.MeasureString(_verificationText, _font);
            _g.DrawString(_verificationText, _font, _brush, (_width - _verificationTextSize.Width) / 2, (_height - _verificationTextSize.Height) / 2);
            _image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return null;
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public JsonResult Exists(string UserName)
        {
            var _result = userRsy.Exists(UserName);
            return Json(_result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (Request.Cookies["User"] != null)
            {
                HttpCookie _cookie = Request.Cookies["User"];
                _cookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(_cookie);
            }
            Notice _n = new Notice { Title = "成功退出", Details = "您已经成功退出！", DwellTime = 5, NavigationName="网站首页", NavigationUrl = Url.Action("Index", "Home") };
            return RedirectToAction("Notice", "Prompt", _n);
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        public static string UserName { 
            get {
                HttpCookie _cookie = System.Web.HttpContext.Current.Request.Cookies["User"];
                if (_cookie == null) return "";
                else return _cookie["UserName"];
                }
        }

        //以下局部视图
    }
}
