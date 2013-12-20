using Ninesky.Models;

namespace Ninesky.Repository
{
    /// <summary>
    /// 模块接口
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.12.10
    /// </remarks>
    /// </summary>
    interface InterfaceModule
    {
        /// <summary>
        /// 查找模块
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns>模块</returns>
        Module Find(int moduleId);
        /// <summary>
        /// 查找模块
        /// </summary>
        /// <param name="model">模型名</param>
        /// <returns>模块</returns>
        Module Find(string model);
    }
}
