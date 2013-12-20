using Ninesky.Models;
using System.Linq;
using System.Web.Mvc;

namespace Ninesky.Repository
{
    public class CommonModelRepository:RepositoryBase<CommonModel>
    {
        public override bool Add(CommonModel cModel)
        {
            dbContext.CommonModels.Add(cModel);
            return dbContext.SaveChanges() > 0;
        }
        public override bool Update(CommonModel cModel)
        {
            dbContext.CommonModels.Attach(cModel);
            dbContext.Entry<CommonModel>(cModel).State = System.Data.EntityState.Modified;
            return dbContext.SaveChanges() > 0;
        }
        public override bool Delete(int cModelId)
        {
            dbContext.CommonModels.Remove(dbContext.CommonModels.SingleOrDefault(m => m.CommonModelId == cModelId));
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 获取分页公共模型内容列表
        /// </summary>
        /// <param name="categoryId">栏目Id</param>
        /// <param name="cChildren">是否包含子栏目</param>
        /// <param name="model">模型名称</param>
        /// <param name="userName">用户名</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="order">排序方式</param>
        /// <returns>分页数据</returns>
        public PagerData<CommonModel> List(int categoryId, bool cChildren,string model, string userName, int currentPage, int pageSize, int order)
        {
            PagerConfig _pConfig = new PagerConfig { CurrentPage = currentPage, PageSize = pageSize };
            var _cModels = dbContext.CommonModels.Include("Category").AsQueryable();
            if (categoryId != 0)
            {
                if (cChildren)//包含子栏目
                {
                    CategoryRepository _cRsy = new CategoryRepository();
                    IQueryable<int> _children = _cRsy.Children(categoryId, 0).Select(c => c.CategoryId);
                    _cModels = _cModels.Where(m => _children.Contains(m.CategoryId));
                }
                else _cModels = _cModels.Where(m => m.CategoryId == categoryId);//不包含子栏目
            }
            if (!string.IsNullOrEmpty(model)) _cModels = _cModels.Where(m => m.Model == model);
            if (!string.IsNullOrEmpty(userName))_cModels = _cModels.Where(m => m.Inputer == userName);
            _pConfig.TotalRecord = _cModels.Count();//总记录数
            //排序
            switch (order)
            {
                case 1://id降序
                    _cModels = _cModels.OrderByDescending(m => m.CommonModelId);
                    break;
                case 2://Id升序
                    _cModels = _cModels.OrderBy(m => m.CommonModelId);
                    break;
                case 3://发布日期降序
                    _cModels = _cModels.OrderByDescending(m => m.ReleaseDate);
                    break;
                case 4://发布日期升序
                    _cModels = _cModels.OrderBy(m => m.ReleaseDate);
                    break;
                case 5://点击降序
                    _cModels = _cModels.OrderByDescending(m => m.Hits);
                    break;
                case 6://点击升序
                    _cModels = _cModels.OrderBy(m => m.Hits);
                    break;
                default://默认id降序
                    _cModels = _cModels.OrderByDescending(m => m.CommonModelId);
                    break;
            }
            //分页
            _cModels = _cModels.Skip((_pConfig.CurrentPage - 1) * _pConfig.PageSize).Take(_pConfig.PageSize);
            PagerData<CommonModel> _pData = new PagerData<CommonModel>(_cModels, _pConfig);
            return _pData;
        }
    }
}