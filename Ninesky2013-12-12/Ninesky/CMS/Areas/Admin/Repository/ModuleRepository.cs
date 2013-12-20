using Ninesky.Models;
using System.Linq;

namespace Ninesky.Areas.Admin.Repository
{
    /// <summary>
    /// 模块接口实例
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.12.12
    /// </remarks>
    /// </summary>
    public class ModuleRepository:Ninesky.Repository.ModuleRepository,InterfaceModule
    {
        public IQueryable<Module> Find()
        {
            return nContext.Modules;
        }

        public bool Modify(Module module)
        {
            nContext.Modules.Attach(module);
            nContext.Entry<Module>(module).State = System.Data.EntityState.Modified;
            return nContext.SaveChanges() > 0;
        }
    }
}