using Ninesky.Models;
using Ninesky.Models.Ui;//该条计划删除
using Ninesky.Ui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ninesky.Repository
{
    /// <summary>
    /// 栏目接口实现类
    /// <remarks>
    /// 版本v1.0
    /// 修改2013.11.17
    /// </remarks>
    /// </summary>
    public class CategoryRepository:InterfaceCategory
    {
        private NineskyContext nineskyContext;
        public CategoryRepository()
        {
            nineskyContext = new NineskyContext();
        }

        public IQueryable<Category> Children(int categoryId, int type)
        {
            return nineskyContext.Categorys.Where(c => c.ParentId == categoryId && c.Type == type).OrderBy(c => c.Order);
        }
        
        public IQueryable<Category> Children(int categoryId)
        {
            return nineskyContext.Categorys.Where(c => c.ParentId == categoryId).OrderBy(c => c.Order);
        }

        public Category Find(int Id)
        {
            return nineskyContext.Categorys.AsNoTracking().SingleOrDefault(c => c.CategoryId == Id);
        }

        public IQueryable<Category> Progeny(int categoryId, int type)
        {
            if (categoryId == 0) return nineskyContext.Categorys.Where(c => c.ParentPath.IndexOf("0") == 0 && c.Type == type).OrderBy(c => c.Order);
            else
            {
                var _category = nineskyContext.Categorys.SingleOrDefault(c => c.CategoryId == categoryId);
                if (_category != null) return nineskyContext.Categorys.Where(c => c.ParentPath.IndexOf(_category.ParentPath) == 0 && c.Type == type).OrderBy(c => c.Order);
                else return null;
            }
        }

        public IQueryable<Category> Progeny(int categoryId)
        {
            if (categoryId == 0) return nineskyContext.Categorys.Where(c => c.ParentPath.IndexOf("0") == 0).OrderBy(c => c.Order);
            else
            {
                var _category = nineskyContext.Categorys.SingleOrDefault(c => c.CategoryId == categoryId);
                if (_category != null) return nineskyContext.Categorys.Where(c => c.ParentPath.IndexOf(_category.ParentPath) == 0).OrderBy(c => c.Order);
                else return null;
            }
        }


        /// <summary>
        /// 目标栏目是否是本身或下级栏目
        /// </summary>
        /// <param name="categoryId">栏目id</param>
        /// <param name="targetCategoryId">目标栏目id</param>
        /// <returns></returns>
        public bool IsSelfOrLower(int categoryId, int targetCategoryId)
        {
            if (categoryId == targetCategoryId) return true;
            if (targetCategoryId == 0) return false;
            return Find(targetCategoryId).ParentPath.IndexOf(Find(categoryId).ParentPath + "," + categoryId) == 0;
        }
        /// <summary>
        /// 栏目列表
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns></returns>
        public IQueryable<Category> List(string model)
        {
            return nineskyContext.Categorys.Where(c => c.Model == model).OrderBy(c => c.Order);
        }
        /// <summary>
        /// 普通栏目树形类表递归函数
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private Tree RecursionTreeGeneral(Tree tree)
        {
            var _children = Children(tree.id, 0).Select(c => new Tree { id = c.CategoryId, text = c.Name, iconCls = "icon-general" }).ToList();
            if (_children != null)
            {

                for (int i = 0; i < _children.Count(); i++)
                {
                    _children[i] = RecursionTreeGeneral(_children[i]);
                }
                tree.children = _children;
            }
            return tree;
        }
        /// <summary>
        /// 获取跟栏目
        /// </summary>
        /// <returns></returns>
        public IQueryable<Category> Root()
        {
            return Children(0);
        }
        /// <summary>
        /// 普通栏目类树形表
        /// </summary>
        /// <returns></returns>
        public List<Tree> TreeGeneral()
        {
            var _root = Children(0, 0).Select(c => new Tree { id = c.CategoryId, text = c.Name, iconCls = "icon-general" }).ToList();
            if (_root != null)
            {
                for (int i = 0; i < _root.Count(); i++)
                {
                    _root[i] = RecursionTreeGeneral(_root[i]);
                }
            }
            return _root;
        }
        /// <summary>
        /// 普通栏目类树形表
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns></returns>
        public List<ZtreeNode> TreeGeneral(string model)
        {
            //查找所有类型为model的栏目
            List<ZtreeNode> _trees = new List<ZtreeNode>();//栏目树
            var _ctemp = nineskyContext.Categorys.AsNoTracking().Where(c => c.Model == model);
            Dictionary<int, Category> _categorys = new Dictionary<int, Category>();
            string _parentPath = string.Empty;
            foreach (var _c in _ctemp)
            {
                _parentPath += "," + _c.ParentPath;
                _categorys.Add(_c.CategoryId, _c);
            }
            //生成父栏目Id列表
            var _strParentIds = _parentPath.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int _id;
            foreach (var _str in _strParentIds)
            {
                if (_str == "0") continue;
                if (int.TryParse(_str, out _id))
                {
                    if (!_categorys.ContainsKey(_id)) _categorys.Add(_id, Find(_id));
                }
            }
            var _root = _categorys.Values.Where(c => c.ParentId == 0).OrderBy(c => c.Order);
            ZtreeNode _tNode;
            foreach (var c in _root)
            {
                _tNode = new ZtreeNode { id = c.CategoryId, name = c.Name };
                if (c.Model == model) _tNode.iconSkin = "canadd";
                else _tNode.iconSkin = "cantadd";
                //子栏目
                if (_categorys.Values.Any(cg => cg.ParentId == _tNode.id))
                {
                    _tNode = TreeGeneralRecursion(model, _tNode,_categorys.Values.ToList());
                }
                _trees.Add(_tNode);
            }
            return _trees;
        }
        /// <summary>
        /// TreeGeneral(string model)的递归函数
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <param name="treeNode">节点</param>
        /// <param name="source">数据源</param>
        private ZtreeNode TreeGeneralRecursion(string model,ZtreeNode treeNode, List<Category> source)
        {
            ZtreeNode _tNode;
            treeNode.children = new List<ZtreeNode>();
            var _children = source.Where(c=>c.ParentId == treeNode.id).OrderBy(c=>c.Order);
            foreach (var _c in _children)
            {
                _tNode = new ZtreeNode { id = _c.CategoryId, name = _c.Name };
                if (_c.Model == model) _tNode.iconSkin = "canadd";
                else _tNode.iconSkin = "cantadd";
                //子栏目
                if (source.Any(cg => cg.ParentId == _tNode.id))
                {
                    _tNode = TreeGeneralRecursion(model, _tNode, source);
                }
                treeNode.children.Add(_tNode);
            }
            return treeNode;
        }
        /// <summary>
        ///  更新栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <returns></returns>
        public bool Update(Category category)
        {
            nineskyContext.Categorys.Attach(category);
            nineskyContext.Entry<Category>(category).State = System.Data.EntityState.Modified;
            if (nineskyContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        /// 更新栏目的ParentParth
        /// </summary>
        /// <param name="oldPath">原来的ParentParth</param>
        /// <param name="newPath">新的ParentParth</param>
        public bool UpdateCategorysParentPath(string oldPath, string newPath)
        {
            var _categorys = nineskyContext.Categorys.Where(c => c.ParentPath.IndexOf(oldPath) == 0);
            foreach (var _c in _categorys)
            {
                _c.ParentPath = newPath + _c.ParentPath.Remove(0, oldPath.Length);
            }
            return nineskyContext.SaveChanges() > 0;
        }
    }
}