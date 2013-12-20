using Ninesky.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ninesky.Repository
{
    /// <summary>
    /// 模块存取数据库代码
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.12.10
    /// 修改2013.12.12
    /// </remarks>
    /// </summary>
    public class ModuleRepository:InterfaceModule
    {
        protected NineskyContext nContext = new NineskyContext();

        public Module Find(int moduleId)
        {
            return nContext.Modules.SingleOrDefault(m => m.ModuleId == moduleId);
        }

        public Module Find(string model)
        {
            return nContext.Modules.SingleOrDefault(m => m.Model == model);
        }

        /// <summary>
        /// ------需修改
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public IQueryable<Module> List(bool enable)
        {
            List<Module> _module = new List<Module>(1);
            _module.Add(new Module { Name = "文章模块", Model = "Article", Enable = true, Description = "文章模块" });
            return _module.AsQueryable();
        }
    }
}