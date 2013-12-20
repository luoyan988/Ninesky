using Ninesky.Models;
using System.Linq;

namespace Ninesky.Repository
{
    /// <summary>
    /// 栏目接口
    /// <remarks>
    /// 版本v1
    /// 创建2013.11.14
    /// </remarks>
    /// </summary>
    interface InterfaceCategory
    {
        /// <summary>
        /// 获取子栏目列表
        /// </summary>
        /// <param name="categoryId">栏目Id</param>
        /// <param name="type">栏目类型</param>
        /// <returns>子栏目列表</returns>
        IQueryable<Category> Children(int categoryId, int type);

        /// <summary>
        /// 获取子栏目列表
        /// </summary>
        /// <param name="categoryId">栏目Id</param>
        /// <returns>子栏目列表</returns>
        IQueryable<Category> Children(int categoryId);

        /// <summary>
        /// 查找栏目
        /// </summary>
        /// <param name="Id">栏目Id</param>
        /// <returns>栏目</returns>
        Category Find(int Id);

        /// <summary>
        /// 获取后代栏目列表
        /// </summary>
        /// <param name="categoryId">栏目Id</param>
        /// <param name="type">栏目类型</param>
        /// <returns>后代栏目列表</returns>
        IQueryable<Category> Progeny(int categoryId,int type);

        /// <summary>
        /// 获取后代栏目列表
        /// </summary>
        /// <param name="categoryId">栏目Id</param>
        /// <returns>后代栏目列表</returns>
        IQueryable<Category> Progeny(int categoryId);
    }
}
