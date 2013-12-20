using Ninesky.Areas.Admin.Models;
using System.Collections.Generic;

namespace Ninesky.Areas.Admin.Repository
{
    public interface InterfaceAdministrator
    {
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="admin">管理员</param>
        /// <returns></returns>
        bool Add(Administrator admin);
        /// <summary>
        /// 更改管理员信息
        /// </summary>
        /// <param name="admin">管理员</param>
        bool Modify(Administrator admin);
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="adminId">管理员Id</param>
        bool Delete(int adminId);
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="admin">管理员</param>
        bool Delete(Administrator admin);
        /// <summary>
        /// 验证管理员账号、密码【返回值-1此管理员不存在，0密码错误，1验证通过】
        /// </summary>
        /// <param name="adminName">用户名</param>
        /// <param name="passWord">密码【加密】</param>
        int Authentication(string userName, string passWord);
        /// <summary>
        /// 查找管理员
        /// </summary>
        /// <param name="adminId">管理员Id</param>
        Administrator Find(int adminId);
        /// <summary>
        /// 查找管理员
        /// </summary>
        /// <param name="adminName">管理员名称</param>
        /// <returns></returns>
        Administrator Find(string adminName);
        /// <summary>
        /// 查找全部管理员
        /// </summary>
        List<Administrator> Find();
    }
}