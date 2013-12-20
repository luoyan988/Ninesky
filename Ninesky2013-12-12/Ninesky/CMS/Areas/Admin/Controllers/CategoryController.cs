using Ninesky.Areas.Admin.Extensions;
using Ninesky.Areas.Admin.Repository;
using Ninesky.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ninesky.Areas.Admin.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.11.14
    /// 修改2013.11.27
    /// </remarks>
    /// </summary>
    [AdminAuthorize]
    public class CategoryController : Controller
    {
        private InterfaceCategory categoryRepository;
        public CategoryController()
        {
            categoryRepository = new CategoryRepository();
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <returns>局部视图</returns>
        public PartialViewResult Add()
        {
            return PartialView(new Category { CategoryView = "Index", ContentOrder = 0, ContentView = "Detail", Model = "", Order = 0, PageSize = 20, ParentId = 0, RecordName = "文章", RecordUnit = "篇", Type = 0 });
        }

        [HttpPost]
        public JsonResult Add(Category category)
        {
            JsonViewModel _jdata = new JsonViewModel();
            if (ModelState.IsValid)//模型验证通过
            {
                //父栏目
                if (category.ParentId == 0) category.ParentPath = "0";
                else
                {
                    var _parentCategory = categoryRepository.Find(category.ParentId);
                    if (_parentCategory == null) ModelState.AddModelError("ParentId", "父栏目不存在。");
                    else if (_parentCategory.Type != 0) ModelState.AddModelError("ParentId", "父栏目不是常规栏目，不能添加子栏目。");
                    else category.ParentPath = _parentCategory.ParentPath + "," + _parentCategory.CategoryId;
                }
                //根据栏目类型验证字段
                switch (category.Type)
                {
                    case 0://常规栏目
                        if (string.IsNullOrEmpty(category.Model))//模型为空
                        {
                            category.ContentView = category.LinkUrl = category.RecordUnit = category.RecordName = null;
                            category.ContentOrder = category.PageSize = null;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "栏目视图不能为空。");
                            if (string.IsNullOrEmpty(category.ContentView)) ModelState.AddModelError("ContentView", "内容视图不能为空。");
                            if (category.ContentOrder == null) ModelState.AddModelError("ContentOrder", "内容排序方式不能为空。");
                            if (category.PageSize == null) ModelState.AddModelError("PageSize", "每页记录数不能为空。");
                            if (string.IsNullOrEmpty(category.RecordUnit)) ModelState.AddModelError("RecordUnit", "记录单位不能为空。");
                            if (string.IsNullOrEmpty(category.RecordName)) ModelState.AddModelError("RecordName", "记录名称不能为空。");
                            category.LinkUrl = null;
                        }
                        break;
                    case 1://单页栏目
                        if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "栏目视图不能为空。");;
                        category.Model = category.ContentView = category.LinkUrl = category.RecordUnit = category.RecordName = null;
                        category.ContentOrder = category.PageSize = null;
                        break;
                    case 2:
                        if (string.IsNullOrEmpty(category.LinkUrl)) ModelState.AddModelError("LinkUrl", "链接地址不能为空。");
                        category.Model = category.CategoryView = category.ContentView = category.RecordUnit = category.RecordName =category.ContentView = null;
                        category.ContentOrder = category.PageSize = null;
                        break;
                }
                //存在逻辑验证错误
                if (!ModelState.IsValid) return Json(new JsonViewModel(ModelState));
                else
                {
                    if (categoryRepository.Add(category)) return Json(new JsonViewModel() { Success = true, Message = "添加栏目成功！" });
                    else return Json(new JsonViewModel() { Success = false, Message = "未能保存导数据库。" });
                }
            }
            //模型验证失败
            else return Json(new JsonViewModel(ModelState));
        }
 
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="id">栏目Id</param>
        /// <returns>Json类型数据</returns>
        [HttpPost]
        public JsonResult Del(int id)
        {
            JsonViewModel _jsongviewModel = new JsonViewModel(){ Authentication=0, ValidationList= new Dictionary<string,string>()};
            //栏目不存在
            if (categoryRepository.Find(id) == null)
            {
                _jsongviewModel.Success = false;
                _jsongviewModel.Message = "栏目不存在，请确认栏目是否已经删除。";
            }
            //存在子栏目
            else if (categoryRepository.Children(id).Count() > 0)
            {
                _jsongviewModel.Success = false;
                _jsongviewModel.Message = "该栏目存在子栏目，请先删除子栏目。";
            }
            //判断是否存在内容（预留）

            //执行删除
            else
            {

                if (categoryRepository.Delete(id))
                {
                    _jsongviewModel.Success = true;
                    _jsongviewModel.Message = "删除成功。";
                }
                else
                {
                    _jsongviewModel.Success = false;
                    _jsongviewModel.Message = "未知错误，未能从数据库中删除栏目。";
                }
            }
            return Json(_jsongviewModel);
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns>局部视图</returns>
        public PartialViewResult Menu()
        {
            return PartialView();
        }

        /// <summary>
        /// 修改栏目
        /// </summary>
        /// <param name="id">栏目Id</param>
        /// <returns>局部视图</returns>
        public PartialViewResult Modify(int id)
        {
            return PartialView(categoryRepository.Find(id));
        }

        [HttpPost]
        public JsonResult Modify(Category category)
        {

            JsonViewModel _jdata = new JsonViewModel();
            if (ModelState.IsValid)//模型验证通过
            {
                var _category = categoryRepository.Find(category.CategoryId);
                //父栏目
                if (category.ParentId == 0) category.ParentPath = "0";
                else
                {
                    var _parentCategory = categoryRepository.Find(category.ParentId);
                    if (_parentCategory == null) ModelState.AddModelError("ParentId", "父栏目不存在。");
                    else if (_parentCategory.Type != 0) ModelState.AddModelError("ParentId", "父栏目不是常规栏目，不能添加子栏目。");
                    //此处验证父栏目不能是其本身
                    else if (category.ParentId  == category.CategoryId) ModelState.AddModelError("ParentId", "父栏目不能是其本身。");
                    //此处验证父栏目不能是其子栏目
                    else if (_parentCategory.ParentPath.IndexOf(_category.ParentPath + "," + _category.CategoryId) == 0) ModelState.AddModelError("ParentId", "父栏目不能是其子栏目。");
                    //设置父栏目路径
                    else category.ParentPath = _parentCategory.ParentPath + "," + _parentCategory.CategoryId;
                }
                //根据栏目类型验证字段
                switch (category.Type)
                {
                    case 0://常规栏目
                        if (string.IsNullOrEmpty(category.Model))//模型为空
                        {
                            category.ContentView = category.LinkUrl = category.RecordUnit = category.RecordName = null;
                            category.ContentOrder = category.PageSize = null;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "栏目视图不能为空。");
                            if (string.IsNullOrEmpty(category.ContentView)) ModelState.AddModelError("ContentView", "内容视图不能为空。");
                            if (category.ContentOrder == null) ModelState.AddModelError("ContentOrder", "内容排序方式不能为空。");
                            if (category.PageSize == null) ModelState.AddModelError("PageSize", "每页记录数不能为空。");
                            if (string.IsNullOrEmpty(category.RecordUnit)) ModelState.AddModelError("RecordUnit", "记录单位不能为空。");
                            if (string.IsNullOrEmpty(category.RecordName)) ModelState.AddModelError("RecordName", "记录名称不能为空。");
                            category.LinkUrl = null;
                        }
                        break;
                    case 1://单页栏目
                        if (categoryRepository.Children(category.CategoryId).Count() != 0) ModelState.AddModelError("Type", "存在子栏目，无法将栏目类型设为单页栏目。");
                        if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "栏目视图不能为空。");
                        category.Model = category.ContentView = category.LinkUrl = category.RecordUnit = category.RecordName = null;
                        category.ContentOrder = category.PageSize = null;
                        break;
                    case 2:
                        if (categoryRepository.Children(category.CategoryId).Count() != 0) ModelState.AddModelError("Type", "存在子栏目，无法将栏目类型设为外部链接。");
                        if (string.IsNullOrEmpty(category.LinkUrl)) ModelState.AddModelError("LinkUrl", "链接地址不能为空。");
                        category.Model = category.CategoryView = category.ContentView = category.RecordUnit = category.RecordName = category.ContentView = null;
                        category.ContentOrder = category.PageSize = null;
                        break;
                }
                //存在逻辑验证错误
                if (!ModelState.IsValid) return Json(new JsonViewModel(ModelState));
                else
                {
                    if (categoryRepository.Modify(category))
                    {
                        //判断父栏目是否更改
                        if (category.ParentId != _category.ParentId) categoryRepository.ChangeChildrenParentParth(_category.ParentPath + "," + category.CategoryId, category.ParentPath + "," + category.CategoryId);
                        return Json(new JsonViewModel() { Success = true, Message = "修改栏目成功！" });
                    }
                    else return Json(new JsonViewModel() { Success = false, Message = "数据未发生更改。" });
                }
            }
            //模型验证失败
            else return Json(new JsonViewModel(ModelState));
        }

        /// <summary>
        /// 栏目树
        /// </summary>
        /// <param name="categoryId">栏目Id，调用所有栏目为0</param>
        /// <param name="categoryType">栏目类型-1所有栏目，0常规栏目</param>
        /// <param name="jsonType">返回的数据类型【默认（0）-EasyuiTree类型】</param>
        /// <returns>Json类型数据</returns>
        public JsonResult Tree(int categoryId = 0, int categoryType = 0, int jsonType = 0)
        {
            IQueryable<Category> _categorys;
            if (categoryType == -1) _categorys = categoryRepository.Progeny(categoryId).OrderByDescending(c => c.ParentPath).ThenBy(c => c.Order);
            else _categorys = categoryRepository.Progeny(categoryId, categoryType).OrderByDescending(c => c.ParentPath).ThenBy(c => c.Order);
            switch (jsonType)
            {
                default:
                    List<EasyuiTreeNodeViewModel> _trees = new List<EasyuiTreeNodeViewModel>();
                    Dictionary<int, EasyuiTreeNodeViewModel> _nodes = new Dictionary<int, EasyuiTreeNodeViewModel>();
                    foreach (var _category in _categorys)
                    {
                        if (_trees.Exists(n => n.parentid == _category.CategoryId))//存在子节点
                        {
                            var _childern = _trees.Where(n => n.parentid == _category.CategoryId).ToList();
                            _trees.RemoveAll(n => n.parentid == _category.CategoryId);
                            _trees.Add(new EasyuiTreeNodeViewModel { parentid = _category.ParentId, id = _category.CategoryId, text = _category.Name, state = "open", children = _childern });
                        }
                        else _trees.Add(new EasyuiTreeNodeViewModel { parentid = _category.ParentId, id = _category.CategoryId, text = _category.Name, state = "close"});
                    }
                    return Json(_trees);
            }
        }
    }
}
