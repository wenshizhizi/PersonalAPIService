
namespace System
{
    using Collections.Generic;
    using Linq.Expressions;
    using Net;
    using Net.Http;
    using Newtonsoft.Json;
    using Runtime.CompilerServices;
    using Text.RegularExpressions;
    using Threading.Tasks;

    /// <summary>
    /// 响应工具，主要用来创建异步的响应
    /// </summary>
    public class ResponseUntil
    {
        /// <summary>
        /// 异步创建响应
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">对象</param>
        /// <returns>结果</returns>
        public static async Task<HttpResponseMessage> CreateResponseAsync<T>(T t)
        {
            return await Task.Run(() =>
            {
                // 约定以json响应
                var responseText = JsonConvert.SerializeObject(t);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(responseText)
                };
            });
        }

        /// <summary>
        /// 异步创建一个错误响应
        /// </summary>
        /// <param name="errMsg">错误信息</param>
        /// <returns>结果</returns>
        public static async Task<HttpResponseMessage> CreateErrResponseAsycn(string errMsg)
        {
            var response = new ResponseEntity<string>
            {
                Msg = errMsg,
                IsSucceed = false
            };

            return await response.CreateResponseAsync();
        }
    }

    /// <summary>
    /// 响应的实体，响应的所有数据都采用这个实体结构，返回为json对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseEntity<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty(PropertyName = "succeed", NullValueHandling = NullValueHandling.Include, Required = Required.Always)]
        public bool IsSucceed { get; set; } = false;

        /// <summary>
        /// 响应的数据
        /// </summary>
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public T Data { get; set; } = default(T);

        /// <summary>
        /// 失败时的消息
        /// </summary>
        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public string Msg { get; set; } = null;

        /// <summary>
        /// 创建一个响应
        /// </summary>
        /// <returns>响应结果</returns>
        public async Task<HttpResponseMessage> CreateResponseAsync()
        {
            return await Task.Run(() =>
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(this))
                };
            });
        }
    }

    /// <summary>
    /// 一些扩展的方法
    /// </summary>
    public static class Extends
    {
        /// <summary>
        /// 扩展方法：校验字符串是否是中文
        /// </summary>
        /// <param name="strValue">字符串内容</param>
        /// <returns>校验结果</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool CheckStringChineseRegex(this string strValue)
        {
            return Regex.IsMatch(strValue, @"[\u4e00-\u9fbb]+$");
        }

        /// <summary>
        /// 扩展方法：校验字符串是否是英文
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool IsLetter(this string strValue)
        {
            return Regex.IsMatch(strValue, @"[a-zA-Z]+");
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="param">参数</param>
        /// <returns>格式化的字符串</returns>
        public static string FormatString(this string value, params string[] param)
        {
            try
            {
                return string.Format(value, param);
            }
            catch (System.Exception)
            {
                return "FormatString Error";
            }
        }

        /// <summary>
        /// 根据表达式，将数字对象转换成以指定分隔符分隔的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="separator"></param>
        /// <param name="express"></param>
        /// <returns></returns>
        public static string StringJoin<T>(this List<T> collection, string separator, Expression<Func<List<T>, List<string>>> express)
        {
            try
            {
                return string.Join(separator, express.Compile().Invoke(collection));
            }
            catch (Exception)
            {
                return "StringJoin error";
            }
        }

        /// <summary>
        /// 判断字符串是否是带小数的数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        /// <summary>
        /// 校验字符串是否是空格或者空或者空字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>校验结果</returns>
        public static bool CheckNullOrEmptyOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 获取枚举的项的字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetRequestTypeString(this RequestType type)
        {
            return type.ToString();
        }

        /// <summary>
        /// 根据值获取请求类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static RequestType ParseRequestType(this int value)
        {
            try
            {
                return (RequestType)Enum.Parse(typeof(RequestType), value.ToString(), true);
            }
            catch
            {
                return RequestType.EnglishWordsStudy;
            }
        }        
    }

    /// <summary>
    /// 处理消息的类型
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// 英语单词学习
        /// </summary>
        EnglishWordsStudy = 0,
        /// <summary>
        /// 英语短语学习
        /// </summary>
        EnglishPhraseStudy = 1,
        /// <summary>
        /// c#文章
        /// </summary>
        CSharp = 2,
        /// <summary>
        /// js文章
        /// </summary>
        Javascript = 3,
        /// <summary>
        /// css文章
        /// </summary>
        CSS = 4,
        /// <summary>
        /// html文章
        /// </summary>
        Html = 5,
        /// <summary>
        /// 记录技术性文章
        /// </summary>
        TechnicalArticlesCollect = 6,
        /// <summary>
        /// 编程技术专业词组
        /// </summary>
        ProgrammingPhrase = 7
    }
}
