using Ninesky.Models;
using System.Linq;

namespace Ninesky.Areas.Admin.Repository
{
    /// <summary>
    /// 后台管理模块接口
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.12.10
    /// </remarks>
    /// </summary>
    interface InterfaceModule:Ninesky.Repository.InterfaceModule
    {
        /// <summary>
        /// 查找模块
        /// </summary>
        /// <returns>所有模块列表</returns>
        IQueryable<Module> Find();

        /// <summary>
        /// 修改模块信息
        /// </summary>
        /// <param name="module">模块</param>
        /// <returns>是否修改成功</returns>
        bool Modify(Module module);
    }
}
