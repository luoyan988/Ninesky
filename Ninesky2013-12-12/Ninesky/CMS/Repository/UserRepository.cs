using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Data.Entity;
using Ninesky.Models;

namespace Ninesky.Repository
{
    public class UserRepository:RepositoryBase<User>
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public override bool Add(User user)
        {
            if (user == null) return false;
            dbContext.Users.Add(user);
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override bool Update(User user)
        {
            dbContext.Users.Attach(user);
            dbContext.Entry<User>(user).State = System.Data.EntityState.Modified;
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public override User Find(int Id)
        {
            return dbContext.Users.SingleOrDefault(u => u.UserId == Id);
        }
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public User Find(string UserName)
        {
            return dbContext.Users.SingleOrDefault(u => u.UserName == UserName);
        }
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool Exists(string UserName)
        {
            if (dbContext.Users.Any(u => u.UserName.ToUpper() == UserName.ToUpper())) return true;
            else return false;
        }
        /// <summary>
        /// 用户验证【0-成功；1-用户名不存在；2-密码错误】
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWrod"></param>
        /// <returns></returns>
        public int Authentication(string UserName, string PassWrod)
        {
            var _user = dbContext.Users.SingleOrDefault(u =>u.UserName == UserName);
            if (_user == null) return 1;
            else if (_user.Password != PassWrod) return 2;
            else return 0;
        }
    }
}