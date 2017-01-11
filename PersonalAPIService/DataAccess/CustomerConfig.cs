

namespace PersonalAPIService.DataAccess
{
    using Entity;    
    using MongoDB.Driver;
    using System;
    using System.Linq;

    public partial class MongodbHelper 
    {
        /// <summary>
        /// 插入一条默认的用户配置
        /// </summary>
        /// <param name="openid">openid</param>
        public static void InsertACustomerConfig(string openid)
        {
            try
            {
                var config = new CustomerConfig
                {
                    NowUseCase = "EnglishWordsStudy",// 默认是英语单词学习
                    OpenId = openid.Trim().ToLower()
                };
                customerConfigCollection.InsertOne(config);
                InsertAnInfoLog("openid为【{0}】的用户关注了订阅号，创建新配置".FormatString(config.OpenId));
            }
            catch (Exception ex)
            {
                InsertError(ex);
            }
        }

        /// <summary>
        /// 根据openid寻找用户配置
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns>用户配置</returns>
        public static CustomerConfig FindCustomerConfigByOpenid(string openid)
        {
            try
            {
                var config = customerConfigCollection.FindAsync(Builders<CustomerConfig>.Filter.Eq(m => m.OpenId, openid)).GetAwaiter().GetResult().FirstOrDefault();

                if (config == default(CustomerConfig))
                {
                    InsertACustomerConfig(openid);
                }
                return config;
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return default(CustomerConfig);
            }
        }

        /// <summary>
        /// 根据openid更新用户配置
        /// </summary>
        /// <param name="openid">openid</param>
        /// <param name="type">操作类型</param>
        /// <returns>更新结果</returns>
        public static bool UpdateCustomerConfigByOpenid(string openid, string type)
        {
            try
            {
                var result = customerConfigCollection.UpdateOneAsync(m => m.OpenId == openid, Builders<CustomerConfig>.Update.Set(m => m.NowUseCase, type)).Result;
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return false;
            }
        }

        /// <summary>
        /// 清除用户的数据，取消订阅后
        /// </summary>
        /// <param name="openid">openid</param>
        public static void CleanCustomerDataByOpenid(string openid)
        {
            try
            {
                // 清除用户配置数据
                customerConfigCollection.DeleteMany(m => m.OpenId == openid.Trim().ToLower());

                // 清除文章数据
                articleCollection.DeleteMany(m => m.OpenId == openid.Trim().ToLower());
                InsertAnInfoLog("openid为【{0}】的用户取消了关注，清除了数据".FormatString(openid.Trim().ToLower()));
            }
            catch (Exception ex)
            {
                InsertError(ex);
            }
        }
    }
}
