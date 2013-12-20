using Ninesky.Areas.Admin.Extensions;
using Ninesky.Models;
using Ninesky.Models.Ui;
using Ninesky.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ninesky.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// 版本v1.0
    /// 修改2013.11.14
    /// </summary>
    public class CategoryController : Controller
    {
        public CategoryController()
        {
            categoryRsy = new CategoryRepository();
        }
        private CategoryRepository categoryRsy;
        #region 前台
        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            var _category = categoryRsy.Find(id);
            if (_category == null)
            {
                Error _e = new Error { Title = "错误", Details = "指定的栏目不存在", Cause = "你访问的栏目已经删除", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("Index", "Home") + "'>网站首页</a></li>") };
                return RedirectToAction("Error", "Prompt", _e);
            }
            if (_category.Type == 2) return Redirect(_category.LinkUrl);
            return View(_category.CategoryView,_category);
        }
        /// <summary>
        /// 根栏目
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialRoot()
        {
            return View(categoryRsy.Root());
        }
        /// <summary>
        /// 子栏目
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <returns></returns> 
        public ActionResult PartialChildren(int id)
        {
            return View(categoryRsy.Children(id));
        }
        /// <summary>
        /// 栏目路径
        /// </summary>
        /// <param name="id">当前栏目Id</param>
        /// <returns></returns>
        public ActionResult PartialPath(int id)
        {
            List<Category> _path = new List<Category>();
            var _category = categoryRsy.Find(id);
            while (_category != null)
            {
                _path.Insert(0, _category);
                _category = categoryRsy.Find(_category.ParentId);      
            }
            return View(_path);
        }
        #endregion
        #region 用户
        /// <summary>
        /// 常规栏目树
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult JsonUserGeneralTree(string model)
        {
            return Json(categoryRsy.TreeGeneral(model));
        }
        #endregion
        #region 管理
        /// <summary>
        /// 管理默认页
        /// </summary>
        [AdminAuthorize]
        public ActionResult ManageDefault()
        {
            return View();
        }
        [AdminAuthorize]
        public ActionResult ManageAdd()
        {
            ModuleRepository _moduleRsy = new ModuleRepository();
            var _modules = _moduleRsy.List(true);
            List<SelectListItem> _slimodule = new List<SelectListItem>(_modules.Count());
            _slimodule.Add(new SelectListItem { Text = "无", Value = "" });
            foreach (Module _module in _modules)
            {
                _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model });
            }
            ViewData.Add("Model", _slimodule);
            ViewData.Add("Type", TypeSelectList);
            ViewData.Add("ContentOrders", ItemModel.ContentOrders);
            return View(new Category());
        }
        [AdminAuthorize]
        [HttpPost]
        public ActionResult ManageAdd(Category category)
        {
            //父栏目是否存在
            if (categoryRsy.Find(category.ParentId) == null) ModelState.AddModelError("ParentId", "父栏目不存在。");
            //ParentPath
            if (category.ParentId == 0) category.ParentPath = "0";
            else category.ParentPath = categoryRsy.Find(category.ParentId).ParentPath + "," + category.ParentId;  
            switch (category.Type)
            {
                case 0://常规栏目
                    if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "×");
                    category.LinkUrl = null;
                    if (!string.IsNullOrEmpty(category.Model))
                    {
                        if (string.IsNullOrEmpty(category.ContentView)) ModelState.AddModelError("ContentView", "×");
                        if (category.ContentOrder == null) category.ContentOrder = 0;
                        if (category.PageSize == null) category.PageSize = 20;
                        if (string.IsNullOrEmpty(category.RecordUnit)) category.RecordUnit = "条";
                        if (string.IsNullOrEmpty(category.RecordName)) category.RecordName = "记录";
                    }
                    else
                    {
                        category.ContentView = null;
                        category.ContentOrder = null;
                        category.PageSize = null;
                        category.RecordUnit = null;
                        category.RecordName = null;
                    }
                    break;
                case 1://单页栏目
                    if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "×");
                    category.LinkUrl = null;
                    category.ContentView = null;
                    category.ContentOrder = null;
                    category.PageSize = null;
                    category.RecordUnit = null;
                    category.RecordName = null;
                    break;
                case 2://外部链接
                    if (string.IsNullOrEmpty(category.LinkUrl)) ModelState.AddModelError("LinkUrl", "×");
                    category.CategoryView = null;
                    category.ContentView = null;
                    category.ContentOrder = null;
                    category.PageSize = null;
                    category.RecordUnit = null;
                    category.RecordName = null;
                    break;
                default:
                    ModelState.AddModelError("Type", "×");
                    break;
            }
            if (ModelState.IsValid)
            {
                //if (categoryRsy.Add(category))
                //{
                //    Notice _n = new Notice { Title = "添加栏目成功", Details = "您已经成功添加[" + category.Name + "]栏目！", DwellTime = 5, NavigationName = "栏目列表", NavigationUrl = Url.Action("ManageDefault", "Category") };
                //    return RedirectToAction("ManageNotice", "Prompt", _n);
                //}
                //else
                //{
                //    Error _e = new Error { Title = "添加栏目失败", Details = "在添加栏目时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ManageAdd", "Category") + "'>添加栏目</a>页面，输入正确的信息后重新操作</li><li>联系网站管理员</li>") };
                //    return RedirectToAction("ManageError", "Prompt", _e);
                //}
                return View();
            }
            else
            {
                ModuleRepository _moduleRsy = new ModuleRepository();
                var _modules = _moduleRsy.List(true);
                List<SelectListItem> _slimodule = new List<SelectListItem>(_modules.Count());
                _slimodule.Add(new SelectListItem { Text = "无", Value = "" });
                foreach (Module _module in _modules)
                {
                    _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model });
                }
                ViewData.Add("Model", _slimodule);
                ViewData.Add("Type", TypeSelectList);
                ViewData.Add("ContentOrders", ItemModel.ContentOrders);
                return View(category);
            }
        }
        /// <summary>
        /// 栏目详细资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ManageDetails(int id)
        {
            var _node = categoryRsy.Find(id);
            if (_node == null)
            {
                Error _e = new Error { Title = "栏目不存在", Details = "栏目不存在", Cause =  Server.UrlEncode("<li>栏目已经删除</li>"), Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ManageDefault", "Category") + "'>栏目栏目管理</a></li>") };
                return RedirectToAction("ManageError", "Prompt", _e);
            }
            ModuleRepository _moduleRsy = new ModuleRepository();
            var _modules = _moduleRsy.List(true);
            List<SelectListItem> _slimodule = new List<SelectListItem>(_modules.Count());
            foreach (Module _module in _modules)
            {
                if (_node.Model == _module.Model) _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model, Selected = true });
                else _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model });
            }
            ViewData.Add("Model", _slimodule);
            var _type = TypeSelectList;
            _type.SingleOrDefault(t => t.Value == _node.Type.ToString()).Selected = true;
            ViewData.Add("Type", _type);
            ViewData.Add("ContentOrders", ItemModel.ContentOrders);
            return View(_node);
        }
        /// <summary>
        /// 修改栏目信息
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public ActionResult ManageUpdate(Category category)
        {
            //父栏目不能为本身或子栏目
            if (categoryRsy.IsSelfOrLower(category.CategoryId,category.ParentId)) ModelState.AddModelError("ParentId", "父栏目不能是其本身或其子栏目");
            switch (category.Type)
            {
                case 0://常规栏目
                    if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "×");
                    category.LinkUrl = null;
                    if (!string.IsNullOrEmpty(category.Model))
                    {
                        if (string.IsNullOrEmpty(category.ContentView)) ModelState.AddModelError("ContentView", "×");
                        if (category.ContentOrder == null) category.ContentOrder = 0;
                        if (category.PageSize == null) category.PageSize = 20;
                        if (string.IsNullOrEmpty(category.RecordUnit)) category.RecordUnit = "条";
                        if (string.IsNullOrEmpty(category.RecordName)) category.RecordName = "记录";
                    }
                    else
                    {
                        category.ContentView = null;
                        category.ContentOrder = null;
                        category.PageSize = null;
                        category.RecordUnit = null;
                        category.RecordName = null;
                    }
                    break;
                case 1://单页栏目
                    if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "×");
                    category.LinkUrl = null;
                    category.ContentView = null;
                    category.ContentOrder = null;
                    category.PageSize = null;
                    category.RecordUnit = null;
                    category.RecordName = null;
                    break;
                case 2://外部链接
                    if (string.IsNullOrEmpty(category.LinkUrl)) ModelState.AddModelError("LinkUrl", "×");
                    category.CategoryView = null;
                    category.ContentView = null;
                    category.ContentOrder = null;
                    category.PageSize = null;
                    category.RecordUnit = null;
                    category.RecordName = null;
                    break;
                default:
                    ModelState.AddModelError("Type", "×");
                    break;
            }
            if (ModelState.IsValid)
            { 
                var _pId = categoryRsy.Find(category.CategoryId).ParentId;
                var _oldParentPath = categoryRsy.Find(category.CategoryId).ParentPath + "," + category.CategoryId;
                //父栏目发生更改
                if (category.ParentId != _pId)
                {
                    //ParentPath
                    if (category.ParentId == 0) category.ParentPath = "0";
                    else category.ParentPath = categoryRsy.Find(category.ParentId).ParentPath + "," + category.ParentId;
                }
                if (categoryRsy.Update(category))
                {
                    Notice _n = new Notice { Title = "修改栏目成功", Details = "修改栏目成功！", DwellTime = 5, NavigationName = "栏目详细信息", NavigationUrl = Url.Action("ManageDetails", "Category", new { id = category.CategoryId }) };
                    if (_oldParentPath != category.ParentPath)
                    {
                        //修改子栏目ParentPath
                        categoryRsy.UpdateCategorysParentPath(_oldParentPath, category.ParentPath+"," + category.CategoryId);
                    }
                    return RedirectToAction("ManageNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "修改栏目失败", Details = "在修改栏目信息时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ManageDetails", "Category", new { id = category.CategoryId }) + "'>栏目详细资料</a>页面，修改信息后重新操作</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
            }
            else
            {
                ModuleRepository _moduleRsy = new ModuleRepository();
                var _modules = _moduleRsy.List(true);
                List<SelectListItem> _slimodule = new List<SelectListItem>(_modules.Count());
                _slimodule.Add(new SelectListItem { Text = "无", Value = "" });
                foreach (Module _module in _modules)
                {
                    _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model });
                }
                ViewData.Add("Model", _slimodule);
                ViewData.Add("Type", TypeSelectList);
                ViewData.Add("ContentOrders", ItemModel.ContentOrders);
                return View("ManageDetails",category);
            }
        }
        /// <summary>
        /// 栏目列表局部树视图
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult ManagePartialTree()
        {
            return View();
        }
        #endregion

        #region json
        [AdminAuthorize]
        public JsonResult JsonTreeParent()
        {
            categoryRsy =new CategoryRepository();
            var _children = categoryRsy.TreeGeneral();
            if (_children == null) _children = new List<Tree>();
            _children.Insert(0, new Tree { id = 0, text = "无",iconCls="icon-general" });
            return Json(_children);
        }
        /// <summary>
        /// 子栏目树形控件Json数据
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <returns></returns>
        [AdminAuthorize]
        public JsonResult ManageTreeChildrenJson(int id = 0)
        {
            var _children = categoryRsy.Children(id);
            List<Tree> _trees = new List<Tree>(_children.Count());
            foreach(var c in _children)
            {
                Tree _t = new Tree { id = c.CategoryId, text = c.Name};
                switch (c.Type)
                {
                    case 0:
                        _t.state = "closed";
                        _t.iconCls = "icon-general";
                        break;
                    case 1:
                        _t.state = "open";
                        _t.iconCls = "icon-page";
                        break;
                    case 2:
                        _t.state = "open";
                        _t.iconCls = "icon-link";
                        break;
                }
                _trees.Add(_t);
            }
            return Json(_trees, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Attribute
        public List<SelectListItem> TypeSelectList
        {
            get
            {
                List<SelectListItem> _items = new List<SelectListItem>();
                _items.Add(new SelectListItem { Text = CategoryType.常规栏目.ToString(), Value = ((int)CategoryType.常规栏目).ToString() });
                _items.Add(new SelectListItem { Text = CategoryType.单页栏目.ToString(), Value = ((int)CategoryType.单页栏目).ToString() });
                _items.Add(new SelectListItem { Text = CategoryType.外部链接.ToString(), Value = ((int)CategoryType.外部链接).ToString() });
                return _items;
            }
        }
        #endregion
    }
}
