using Ninesky.Models;

namespace Ninesky.Areas.Admin.Repository
{
    /// <summary>
    /// 栏目接口
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.11.30
    /// </remarks>
    /// </summary>
    interface InterfaceCategory:Ninesky.Repository.InterfaceCategory
    {
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <returns>布尔值。true表示添加成功，false表示失败。</returns>
        bool Add(Category category);

        /// <summary>
        /// 修改子栏目父路径
        /// </summary>
        /// <param name="originalParth">原路径</param>
        /// <param name="newParh">新路径</param>
        /// <returns>布尔值。true表示添加成功，false表示失败。</returns>
        bool ChangeChildrenParentParth(string originalParth, string newParh);

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="categoryId">栏目Id</param>
        /// <returns>布尔值。true表示删除成功，false表示删除失败。</returns>
        bool Delete(int categoryId);

        /// <summary>
        /// 修改栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <returns>布尔值。true表示修改成功，false表示数据无更改，不需保存。</returns>
        bool Modify(Category category);
    }
}
