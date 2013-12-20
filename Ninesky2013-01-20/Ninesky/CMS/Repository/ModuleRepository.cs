using Ninesky.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ninesky.Repository
{
    public class ModuleRepository
    {
        public IQueryable<Module> List(bool enable)
        {
            List<Module> _module = new List<Module>(1);
            _module.Add(new Module { Name = "文章模块", Model = "Article", Enable = true, Description = "文章模块" });
            return _module.AsQueryable();
        }
    }
}