using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninesky.Models;
using Ninesky.Repository;
using System.Data.EntityClient;

namespace Ninesky.Areas.Admin.Repository
{
    /// <summary>
    /// 栏目实现类
    /// <remarks>
    /// 版本v1
    /// 创建2013.11.13
    /// </remarks>
    /// </summary>
    public class CategoryRepository:Ninesky.Repository.CategoryRepository, InterfaceCategory
    {
        public bool Add(Category category)
        {
            using (NineskyContext _nineskyContext = new NineskyContext())
            {
                _nineskyContext.Categorys.Add(category);
                return _nineskyContext.SaveChanges() > 0;
            }
        }

        public bool ChangeChildrenParentParth(string originalParth, string newParh)
        {
            using (NineskyContext _nineskyContext = new NineskyContext())
            {
                var _children = _nineskyContext.Categorys.Where(c => c.ParentPath.IndexOf(originalParth) == 0);
                foreach(var _child in _children)
                {
                    _child.ParentPath = _child.ParentPath.Replace(originalParth, newParh);
                }
                return _nineskyContext.SaveChanges() > 0;
            }
        }

        public bool Delete(int categoryId)
        {
            using (NineskyContext _nineskyContext = new NineskyContext())
            {
                _nineskyContext.Categorys.Remove(_nineskyContext.Categorys.SingleOrDefault(c => c.CategoryId == categoryId));
                return _nineskyContext.SaveChanges() > 0;
            }
        }

        public bool Modify(Category category)
        {
            using (NineskyContext _nineskyContext = new NineskyContext())
            {
                _nineskyContext.Categorys.Attach(category);
                _nineskyContext.Entry<Category>(category).State = System.Data.EntityState.Modified;
                return _nineskyContext.SaveChanges() > 0;
            }
        }
    }
}