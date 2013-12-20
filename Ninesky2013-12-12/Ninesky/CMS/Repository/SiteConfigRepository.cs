using Ninesky.Models;
using System.Linq;

namespace Ninesky.Repository
{
    /// <summary>
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.8.4
    /// </remarks>
    /// </summary>
    public class SiteConfigRepository:InterfaceSiteConfig
    {
        private NineskyContext nineskyContext;
        /// <summary>
        /// 查找设置
        /// </summary>
        /// <returns></returns>
        public SiteConfig Find()
        {
            using (nineskyContext = new NineskyContext())
            {
                return nineskyContext.SiteConfig.SingleOrDefault();
            }
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="siteConfig">设置</param>
        /// <returns></returns>
        public bool Save(SiteConfig siteConfig)
        {
            using (nineskyContext = new NineskyContext())
            {
                if (nineskyContext.SiteConfig.Count() == 0) nineskyContext.SiteConfig.Add(siteConfig);
                else
                {
                    nineskyContext.SiteConfig.Attach(siteConfig);
                    nineskyContext.Entry<SiteConfig>(siteConfig).State = System.Data.EntityState.Modified;
                }
                return nineskyContext.SaveChanges() > 0;
            }
        }
    }
}