

namespace PersonalAPIService.DataAccess
{
    using Entity;    
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using MongoDB.Bson;
    using System.Threading.Tasks;

    public partial class MongodbHelper 
    {
        /// <summary>
        /// 插入一个英语单词
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns>插入后的英语单词，字段完整</returns>
        public static EnglishWord InsertAnEnglishWord(EnglishWord word)
        {
            try
            {
                if (word.OrderField < 0.3 || word.OrderField > 0.8)
                {
                    word.OrderField = (double)(new Random().Next(3, 8)) / 10;
                }

                englishWordsCollection.InsertOne(word);

                return word;
            }
            catch (MongoWriteException ex)
            {
                if (ex.WriteError.Code == 11000)
                {
                    var _hasWord = FindAnEnglishWordByExpress(m => m.WordText == word.WordText).FirstOrDefault();
                    return _hasWord;
                }
                else
                {
                    return default(EnglishWord);
                }
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return default(EnglishWord);
            }
        }

        /// <summary>
        /// 模糊查找英文单词
        /// </summary>
        /// <param name="fieldExpress">模糊查找条件的字段表达式</param>
        /// <param name="fuzzyValue">模糊匹配的值</param>
        /// <returns>匹配结果</returns>
        public static List<EnglishWord> FuzzCheckEnglishWords(Expression<Func<EnglishWord, object>> fieldExpress, string fuzzyValue)
        {
            try
            {
                return englishWordsCollection.FindAsync(
                        Builders<EnglishWord>.
                        Filter.
                        Regex(
                            fieldExpress,
                            new BsonRegularExpression(fuzzyValue))).
                            GetAwaiter().
                            GetResult().
                            ToList();
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return new List<EnglishWord>();
            }
        }

        /// <summary>
        /// 根据表达式查找一个英文单词
        /// </summary>
        /// <param name="query">表达式</param>
        /// <returns>查找的结果</returns>
        public static List<EnglishWord> FindAnEnglishWordByExpress(Expression<Func<EnglishWord, bool>> query)
        {
            try
            {
                return englishWordsCollection.AsQueryable().Where(query).ToList();
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return new List<EnglishWord>();
            }
        }

        /// <summary>
        /// 以异步的方式查找指定个数的随机单词
        /// </summary>
        /// <param name="randomCount">随机的个数</param>
        /// <param name="openid">openid</param>
        /// <returns>查找结果</returns>
        public static async Task<List<EnglishWord>> FindRandomEnglishWordsSpecifyCountAsync(int randomCount, string openid)
        {
            return await Task.Run(() => {
                try
                {
                    var ret = new List<EnglishWord>();
                    long tick = DateTime.Now.Ticks;
                    Random random = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

                    // 获取上次的随机单词
                    var lastWords = lastRandomWordsCollection.Find(m => m.OpenId == openid).FirstOrDefault();
                    var words = lastWords != default(LastRandomWords) ? lastWords.LastGenerateWords : new List<string>();

                    var tempRangeWords = englishWordsCollection.Aggregate().ToList();

                    for (int i = 0; i < randomCount; i = ret.Count)
                    {
                        var word = tempRangeWords[random.Next(0, tempRangeWords.Count)];
                        if (!ret.Exists(m => m.WordText == word.WordText) && !words.Exists(m => m.Split(new string[] { "   " }, StringSplitOptions.None)[0] == word.WordText)) ret.Add(word);
                    }

                    // 更新上次随机单词，以便下一次随机抽取时参考
                    if (lastWords == default(LastRandomWords))
                        lastRandomWordsCollection.InsertOne(new LastRandomWords
                        {
                            LastGenerateWords = ret.Select(m => "{0}   [{2}]   {1}".FormatString(m.WordText, m.Paraphrase, m.UsPhoneticSymbol)).ToList(),
                            OpenId = openid
                        });
                    else
                        lastRandomWordsCollection.UpdateOne(m => m.Id == lastWords.Id, Builders<LastRandomWords>.Update.Set(m => m.LastGenerateWords, ret.Select(m => "{0}   [{2}]   {1}".FormatString(m.WordText, m.Paraphrase, m.UsPhoneticSymbol)).ToList()));

                    return ret;
                }
                catch (Exception ex)
                {
                    InsertError(ex);
                    return new List<EnglishWord>();
                }
            });
        }

        /// <summary>
        /// 根据表达式删除英文单词
        /// </summary>
        /// <param name="express">指定的表达式</param>
        /// <returns>执行结果</returns>
        public static bool DeleteEnglishWordsByExpress(Expression<Func<EnglishWord, bool>> express)
        {
            try
            {
                return englishWordsCollection.DeleteMany(express).DeletedCount > 0;
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取最后一次生成的、带翻译和读音的随机单词
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns>查询结果</returns>
        public static List<string> GetLastRandomWordsWithTranslateByOpenid(string openid)
        {
            try
            {
                var lastWords = lastRandomWordsCollection.Find(m => m.OpenId == openid).FirstOrDefault();
                var words = lastWords != default(LastRandomWords) ? lastWords.LastGenerateWords : new List<string>();
                return words;
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return new List<string>();
            }
        }

        /// <summary>
        /// 获取英语单词总数
        /// </summary>
        /// <returns></returns>
        public static int GetEnglishDocumentionCount()
        {
            try
            {
                return unchecked((int)englishWordsCollection.CountAsync(m => true).GetAwaiter().GetResult());
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return -1;
            }
        }
    }
}
