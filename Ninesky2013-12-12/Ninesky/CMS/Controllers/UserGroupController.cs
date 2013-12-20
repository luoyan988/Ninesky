using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninesky.Models;
using Ninesky.Repository;
using Ninesky.Areas.Admin.Extensions;

namespace Ninesky.Controllers
{
    public class UserGroupController : Controller
    {
        private UserGroupRepository userGroupRsy;
        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult Add()
        {
            ViewData.Add("Type", TypeSelectList);
            return View();
        }
        [HttpPost]
        [AdminAuthorize]
        public ActionResult Add(UserGroup userGroup)
        {
            userGroupRsy = new UserGroupRepository();
            if (userGroupRsy.Add(userGroup))
            {
                Notice _n = new Notice { Title = "添加用户组成功", Details = "您已经成功添加["+userGroup.Name+"]用户组！", DwellTime = 5, NavigationName = "用户组列表", NavigationUrl = Url.Action("List", "UserGroup") };
                return RedirectToAction("ManageNotice", "Prompt", _n);
            }
            else
            {
                Error _e = new Error { Title = "添加用户组失败", Details = "在添加用户组时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("Add", "UserGroup") + "'>添加用户</a>页面，输入正确的信息后重新操作</li><li>联系网站管理员</li>") };
                return RedirectToAction("ManageError", "Prompt", _e);
            }
        }
        /// <summary>
        /// 修改用户组
        /// </summary>
        /// <param name="Id">用户组Id</param>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult Edit(int Id)
        {
            userGroupRsy = new UserGroupRepository();
            var _userGroup = userGroupRsy.Find(Id);
            return View(_userGroup);
        }
        [HttpPost]
        [AdminAuthorize]
        public ActionResult Eidt(UserGroup userGroup)
        {
            userGroupRsy = new UserGroupRepository();
            var _userGroup = userGroupRsy.Find(userGroup.UserGroupId);
            if (_userGroup == null)
            {
                Error _e = new Error { Title = "用户组不存在", Details = "修改用户时发生错误，修改的用户组不存在。", Cause = "该用户组已被其他管理员删除", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("List", "UserGroup") + "'>用户组列表</a></li>") };
                return RedirectToAction("ManageError", "Prompt", _e);
            }
            _userGroup.Name = userGroup.Name;
            _userGroup.Description = userGroup.Description;
            if (userGroupRsy.Update(_userGroup))
            {
                Notice _n = new Notice { Title = "修改成功", Details = "成功修改了用户组信息", DwellTime = 3, NavigationName = "用户组列表", NavigationUrl = Url.Action("List", "UserGroup") };
                return RedirectToAction("ManageNotice", "Prompt", _n);
            }
            else
            {
                Error _e = new Error { Title = "更新数据失败", Details = "修改用户组信息时修改的信息未能保存到数据库。", Cause = Server.UrlEncode("<li>您并未更改用户组信息。</li><li>数据库未知错误。</li>"), Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("List", "UserGroup") + "'>用户组列表</a></li><li>重新<a href='" + Url.Action("Edit", "UserGroup", new { id = userGroup.UserGroupId }) + "'>修改用户组</a></li>") };
                return RedirectToAction("ManageError", "Prompt", _e);
            }
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="Id">用户组Id</param>
        /// <returns></returns>
        [HttpPost]
        [AdminAuthorize]
        public JsonResult Delete(int Id)
        {
            userGroupRsy = new UserGroupRepository();
            if (userGroupRsy.Delete(Id)) return Json(true);
            else return Json(false);
        }
        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <param name="Id">用户组类型</param>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult List(int Id = -1)
        {
            userGroupRsy = new UserGroupRepository();
            IQueryable<UserGroup> _userGroup;
            if (Id == -1) _userGroup = userGroupRsy.List();
            else _userGroup = userGroupRsy.List(Id);
            var _typeLists = TypeSelectList;
            _typeLists.Insert(0, new SelectListItem { Text = "全部", Value = "-1" });
            if (_typeLists.Any(t => t.Value == Id.ToString())) _typeLists.SingleOrDefault(t => t.Value == Id.ToString()).Selected = true;
            ViewData.Add("GroupTypeList",_typeLists);
            return View(_userGroup);
        }

        /// <summary>
        /// 用户组类型的SelectList列表
        /// </summary>
        public List<SelectListItem> TypeSelectList
        {
            get
            {
                List<SelectListItem> _items = new List<SelectListItem>();
                _items.Add(new SelectListItem { Text = UserGroupType.Anonymous.ToString(), Value = ((int)UserGroupType.Anonymous).ToString() });
                _items.Add(new SelectListItem { Text = UserGroupType.Limited.ToString(), Value = ((int)UserGroupType.Limited).ToString() });
                _items.Add(new SelectListItem { Text = UserGroupType.Normal.ToString(), Value = ((int)UserGroupType.Normal).ToString() });
                _items.Add(new SelectListItem { Text = UserGroupType.Special.ToString(), Value = ((int)UserGroupType.Special).ToString() });
                return _items;
            }
        }
    }
}
