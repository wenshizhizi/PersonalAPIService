namespace EnglishStudyService.API
{    
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Threading.Tasks;

    // 配置控制器路由规则
    [RoutePrefix("e")]
    [Route("{action}")]
    public class EnglishWordsController : ApiController
    {        
        /// <summary>
        /// 默认入口
        /// </summary>       
        /// <returns></returns>        
        [Route("{type}")]
        [HttpGet]    
        [HttpPost]
        public HttpResponseMessage DefaultProccess(int type)
        {
            try
            {                
                return CreateSuccessedResponse("响应成功"); 
            }
            catch (Exception ex)
            {               
                return CreateFialdResponse();
            }
        }
        
        /// <summary>
        /// 返回一个正确的响应
        /// </summary>
        /// <param name="responseContent">响应内容</param>
        /// <returns></returns>
        private HttpResponseMessage CreateSuccessedResponse(string responseContent)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent)
            };
        }

        /// <summary>
        /// 创建一个错误的响应
        /// </summary>        
        /// <returns></returns>
        private HttpResponseMessage CreateFialdResponse()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("-1");
            return response;
        }
    }
}
