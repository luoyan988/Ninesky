using Ninesky.Models;
using System.Linq;

namespace Ninesky.Repository
{
    public class UserGroupRepository:RepositoryBase<UserGroup>
    {
        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns></returns>
        public override bool Add(UserGroup userGroup)
        {
            dbContext.UserGroups.Add(userGroup);
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        /// 更新用户组
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns></returns>
        public override bool Update(UserGroup userGroup)
        {
            dbContext.UserGroups.Attach(userGroup);
            dbContext.Entry<UserGroup>(userGroup).State = System.Data.EntityState.Modified;
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <returns></returns>
        public override bool Delete(int userGroupId)
        {
            dbContext.UserGroups.Remove(dbContext.UserGroups.SingleOrDefault(ug=>ug.UserGroupId == userGroupId));
            if(dbContext.SaveChanges()>0) return true;
            else return false;
        }
        /// <summary>
        /// 查找制定用户组
        /// </summary>
        /// <param name="UserGropuId"></param>
        /// <returns></returns>
        public override UserGroup Find(int UserGropuId)
        {
            return dbContext.UserGroups.SingleOrDefault(ug => ug.UserGroupId == UserGropuId);
        }
        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserGroup> List()
        {
            return dbContext.UserGroups;
        }
        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <param name="userGroupType">用户组类型</param>
        /// <returns></returns>
        public IQueryable<UserGroup> List(int userGroupType)
        {
            return dbContext.UserGroups.Where(ug => ug.Type == (UserGroupType)userGroupType);
        }
    }
}