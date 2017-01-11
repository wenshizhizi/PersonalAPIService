
namespace System
{
    using Collections.Generic;
    using Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Text.RegularExpressions;

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

        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

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
    }
}
