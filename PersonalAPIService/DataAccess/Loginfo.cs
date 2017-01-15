

namespace PersonalAPIService.DataAccess
{
    using Entity;
    using MongoDB.Driver;
    using System;

    public partial class MongodbHelper 
    {
        /// <summary>
        /// 插入一条信息日志
        /// </summary>
        /// <param name="logInfo">日志信息</param>
        public static void InsertAnInfoLog(string logInfo)
        {
            logCollection.InsertOne(new LogInfo
            {
                LogContent = logInfo,
                LogLevel = "info"
            });
        }

        /// <summary>
        /// 插入一条错误信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void InsertError(Exception ex)
        {
            logCollection.InsertOne(new LogInfo
            {
                LogContent = ex.Message,
                LogLevel = "error",
                OtherInfo = ex.StackTrace
            });
        }

        /// <summary>
        /// 查询日志数
        /// </summary>
        /// <returns>结果</returns>
        public static long GetLogDocumentionCount()
        {
            try
            {
                return logCollection.CountAsync(m=> true).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return -1;
            }
        }
    }
}
