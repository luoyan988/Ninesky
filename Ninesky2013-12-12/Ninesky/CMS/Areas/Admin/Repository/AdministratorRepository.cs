
using Ninesky.Areas.Admin.Models;
using Ninesky.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ninesky.Areas.Admin.Repository
{
    public class AdministratorRepository:InterfaceAdministrator
    {
        private NineskyContext nineskyContext;
        public bool Add(Administrator admin)
        {
            using (nineskyContext = new NineskyContext())
            {
                if (nineskyContext.Administrators.Any(a => a.AdminName == admin.AdminName)) return false;
                nineskyContext.Administrators.Add(admin);
                return nineskyContext.SaveChanges() > 0;
            }
            
        }
        public int Authentication(string adminName, string passWord)
        {
            using (nineskyContext = new NineskyContext())
            {
                if (nineskyContext.Administrators.Any(a => a.AdminName == adminName))
                {
                    var _admin = nineskyContext.Administrators.SingleOrDefault(a => a.AdminName == adminName);
                    if (_admin.PassWord == passWord) return 1;
                    else return 0;
                }
                else return -1;
            }
        }
        public bool Delete(int adminId)
        {
            using (nineskyContext = new NineskyContext())
            {
                nineskyContext.Administrators.Remove(nineskyContext.Administrators.SingleOrDefault(a => a.AdministratorId == adminId));
                return nineskyContext.SaveChanges() > 0;
            }
        }
        public bool Delete(Administrator admin)
        {
            using (nineskyContext = new NineskyContext())
            {
                nineskyContext.Administrators.Remove(admin);
                return nineskyContext.SaveChanges() > 0;
            }
        }
        public Administrator Find(int adminId)
        {
            using (nineskyContext = new NineskyContext())
            {
                return nineskyContext.Administrators.SingleOrDefault(a => a.AdministratorId == adminId);
            }
        }
        public Administrator Find(string adminName)
        {
            using (nineskyContext = new NineskyContext())
            {
                return nineskyContext.Administrators.SingleOrDefault(a => a.AdminName == adminName);
            }
        }
        public List<Administrator> Find()
        {
            using (nineskyContext = new NineskyContext())
            {
                return nineskyContext.Administrators.ToList();
            }
        }
        public bool Modify(Administrator admin)
        {
            using (nineskyContext = new NineskyContext())
            {
                nineskyContext.Administrators.Attach(admin);
                nineskyContext.Entry<Administrator>(admin).State = System.Data.EntityState.Modified;
                return nineskyContext.SaveChanges() > 0;
            }
        }
    }
}