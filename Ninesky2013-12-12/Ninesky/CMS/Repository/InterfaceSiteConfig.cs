using Ninesky.Models;

namespace Ninesky.Repository
{
    /// <summary>
    /// 网站设置信息接口
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.8.4
    /// </remarks>
    /// </summary>
    public interface InterfaceSiteConfig
    {
        /// <summary>
        /// 查找设置
        /// </summary>
        /// <returns></returns>
        SiteConfig Find();

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="siteConfig">设置</param>
        /// <returns></returns>
        bool Save(SiteConfig siteConfig);
    }
}
