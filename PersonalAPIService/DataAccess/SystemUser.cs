

namespace PersonalAPIService.DataAccess
{
    using Entity;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using System;

    public partial class MongodbHelper 
    {
        /// <summary>
        /// 插入一个用户
        /// </summary>
        /// <param name="user">用户</param>
        public static void InsertAnSystemUser(SystemUser user)
        {
            try
            {
                systemUserCollection.InsertOneAsync(user).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                InsertError(ex);
            }
        }
                
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns>用户</returns>
        public static SystemUser FindSystemUser(SystemUser user)
        {
            try
            {
                return systemUserCollection.AsQueryable().Where(m => m.Password == user.Password && m.UserName == user.UserName && m.IsDeleted == false ).FirstOrDefaultAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return default(SystemUser);
            }
        }
    }
}
