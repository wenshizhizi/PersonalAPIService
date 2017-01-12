namespace PersonalAPIService.API
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Threading.Tasks;
    using PersonalAPIService.DataAccess;
    using static System.ResponseUntil;

    // 配置控制器路由规则
    [RoutePrefix("e")]
    [Route("{action}")]
    public class EnglishWordsController : ApiController
    {
        /// <summary>
        /// 默认入口
        /// <para>
        /// &gt;&gt;&gt;详情请参见<see cref="RequestType"/>
        /// </para>     
        /// </summary>
        /// <param name="type">请求类型</param>
        /// <returns>响应结果</returns>        
        [Route("{type}")]
        [HttpGet]
        [HttpPost]
        public async Task<HttpResponseMessage> DefaultProccess(int type)
        {
            try
            {
                var requestType = type.ParseRequestType();

                return await CreateResponseAsync("响应成功");
            }
            catch (Exception ex)
            {
                MongodbHelper.InsertError(ex);
                return await CreateErrResponseAsycn(ex.Message);
            }
        }
    }
}
