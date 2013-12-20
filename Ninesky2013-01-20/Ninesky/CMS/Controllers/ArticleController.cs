using Ninesky.Models;
using Ninesky.Repository;
using System.Web.Mvc;

namespace Ninesky.Controllers
{
    public class ArticleController : Controller
    {
        ArticleRepository articleRsy;
        CommonModelRepository cModelRsy;
        public ArticleController()
        {
            articleRsy = new ArticleRepository();
        }
        #region 用户中心
        /// <summary>
        /// 用户默认页
        /// </summary>
        [UserAuthorize]
        public ActionResult UserDefault()
        {
            return View();
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        [UserAuthorize]
        public ActionResult UserAdd()
        {
            return View(new Article() { CommonModel = new CommonModel() });
        }
        [HttpPost]
        [UserAuthorize]
        [ValidateInput(false)]
        public ActionResult UserAdd(Article article)
        {
            //验证栏目
            CategoryRepository _categoryRsy = new CategoryRepository();
            var _category = _categoryRsy.Find(article.CommonModel.CategoryId);
            if (_category == null) ModelState.AddModelError("CommonModel.CategoryId", "栏目不存在");
            if(_category.Model != "Article") ModelState.AddModelError("CommonModel.CategoryId", "该栏目不能添加文章！");
            article.CommonModel.Inputer = UserController.UserName;
            ModelState.Remove("CommonModel.Inputer");
            article.CommonModel.Model = "Article";
            ModelState.Remove("CommonModel.Model");
            if (ModelState.IsValid)
            {
                if (articleRsy.Add(article))
                {
                    Notice _n = new Notice { Title = "添加文章成功", Details = "您已经成功添加[" + article.CommonModel.Title + "]文章！", DwellTime = 5, NavigationName = "我的文章", NavigationUrl = Url.Action("UserOwn", "Article") };
                    return RedirectToAction("UserNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "添加文章失败", Details = "在添加文章时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("UserAdd", "Article") + "'>添加文章</a>页面，输入正确的信息后重新操作</li><li>返回<a href='" + Url.Action("UserDefault", "Article") + "'>文章管理首页</a>。</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
            }
            return View(article);
        }
        [UserAuthorize]
        public ActionResult UserAll(int id = 0, int page = 1)
        {
            int _pageSize = 20;
            int _cOrder = 0;
            Category _c = null;
            cModelRsy = new CommonModelRepository();
            PagerData<CommonModel> _aData;
            if (id > 0)
            {
                var _cRsy = new CategoryRepository();
                _c = _cRsy.Find(id);
                if (_c != null)
                {
                    _pageSize = (int)_c.PageSize;
                    _cOrder = (int)_c.ContentOrder;
                }
            }
            _aData = cModelRsy.List(id, false, "Article", null, page, _pageSize, _cOrder);
            if (_c != null)
            {
                _aData.Config.RecordName = _c.RecordName;
                _aData.Config.RecordUnit = _c.RecordUnit;
            }
            return View(_aData);
        }
        [UserAuthorize]
        public ActionResult UserEdit(int id)
        {
            return View(articleRsy.Find(id));
        }
        [HttpPost]
        [UserAuthorize]
        [ValidateInput(false)]
        public ActionResult UserEdit(Article article)
        {
            //验证栏目
            CategoryRepository _categoryRsy = new CategoryRepository();
            var _category = _categoryRsy.Find(article.CommonModel.CategoryId);
            if (_category == null) ModelState.AddModelError("CommonModel.CategoryId", "栏目不存在");
            if (_category.Model != "Article") ModelState.AddModelError("CommonModel.CategoryId", "该栏目不能添加文章！");
            article.CommonModel.Category = _category;
            article.CommonModel.Inputer = UserController.UserName;
            ModelState.Remove("CommonModel.Inputer");
            article.CommonModel.Model = "Article";
            ModelState.Remove("CommonModel.Model");
            if (ModelState.IsValid)
            {
                articleRsy.Update(article);
                return RedirectToAction("UserDefault");
            }
            return View(article);
        }
        [UserAuthorize]
        public ActionResult UserOwn(int id = 0, int page = 1)
        {
            int _pageSize = 20;
            int _cOrder = 0;
            Category _c = null;
            cModelRsy = new CommonModelRepository();
            PagerData<CommonModel> _aData;
            if (id > 0)
            {
                var _cRsy = new CategoryRepository();
                _c =_cRsy.Find(id);
                if (_c != null)
                {
                    _pageSize = (int)_c.PageSize;
                    _cOrder = (int)_c.ContentOrder;
                }
            }
            _aData = cModelRsy.List(id, false, "Article", UserController.UserName, page, _pageSize, _cOrder);
            if (_c != null)
            {
                _aData.Config.RecordName = _c.RecordName;
                _aData.Config.RecordUnit = _c.RecordUnit;
            }
            return View(_aData);
        }
        /// <summary>
        /// 导航菜单
        /// </summary>
        [UserAuthorize]
        public PartialViewResult PartialUserNavMenus()
        {
            return PartialView();
        }
        #endregion
    }
}
