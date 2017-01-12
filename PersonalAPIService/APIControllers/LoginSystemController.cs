namespace PersonalAPIService.API
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Threading.Tasks;
    using Logic;
    using Newtonsoft.Json;
    using static System.ResponseUntil;

    public class LoginSystemController : ApiController
    {
        /// <summary>
        /// 登录接口
        /// </summary>       
        /// <returns></returns>            
        [HttpPost]
        [HttpGet]
        public async Task<HttpResponseMessage> Index()
        {
            try
            {
                var user = JsonConvert.DeserializeAnonymousType(
                    await Request.Content.ReadAsStringAsync(), new
                    {
                        name = "",
                        password = ""
                    });

                var isLoginSucceed = new LoginLogic().Login(user);

                var result = isLoginSucceed ?
                        new ResponseEntity<string>
                        {
                            IsSucceed = true
                        } :
                        new ResponseEntity<string>
                        {
                            IsSucceed = false,
                            Msg = "登录失败，请检查日志"
                        };

                return await result.CreateResponseAsync();
            }
            catch (Exception ex)
            {
                DataAccess.MongodbHelper.InsertError(ex);

                return await CreateErrResponseAsycn(ex.Message);
            }
        }
    }
}
