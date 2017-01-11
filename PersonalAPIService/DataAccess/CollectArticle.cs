

namespace PersonalAPIService.DataAccess
{
    using Entity;    
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public partial class MongodbHelper
    {
        /// <summary>
        /// 插入一条搜集的文章信息
        /// </summary>
        /// <param name="article">文章信息</param>
        /// <returns>收集结果</returns>
        public static bool InsertAnArticle(CollectedArtical article)
        {
            try
            {
                articleCollection.InsertOne(article);
                if (article.Id != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MongoWriteException ex)
            {
                if (ex.WriteError.Code == 11000)
                {
                    InsertAnInfoLog("已存在相同的文章【{0}】".FormatString(article.ArticleTitle));
                    return true;
                }
                else
                {
                    InsertError(ex);
                    return false;
                }
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据文章标题模糊查询文章
        /// </summary>
        /// <param name="openid">openid</param>
        /// <param name="title">文章标题</param>
        /// <returns>查找结果</returns>
        public static List<CollectedArtical> FuzzCheckArticlesByArticleTitle(string openid, string title)
        {
            try
            {
                var filter = Builders<CollectedArtical>.Filter.Eq(m => m.OpenId, openid) & Builders<CollectedArtical>.Filter.Regex(m => m.ArticleTitle, title);
                return articleCollection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return new List<CollectedArtical>();
            }
        }

        /// <summary>
        /// 根据表达式模糊查询文章
        /// </summary>
        /// <param name="where">查询的表达式</param>
        /// <returns>查询结果</returns>
        public static List<CollectedArtical> FuzzCheckArticlesByArticleTitle(Expression<Func<CollectedArtical, bool>> where)
        {
            try
            {
                return articleCollection.AsQueryable().Where(where).ToList();
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return new List<CollectedArtical>();
            }
        }
    }
}
