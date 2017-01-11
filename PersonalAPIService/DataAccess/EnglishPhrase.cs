

namespace PersonalAPIService.DataAccess
{
    using Entity;    
    using MongoDB.Driver;
    using System;
    using System.Linq;
    using MongoDB.Bson;
    using System.Threading.Tasks;

    public partial class MongodbHelper
    {
        /// <summary>
        /// 根据当前采集语法、短语的步骤更新语法、短语
        /// </summary>
        /// <param name="step">采集单词、短语的步骤</param>
        public static void UpdateEnglishPhrase(StudyPhraseStep step)
        {
            try
            {
                EnglishPhrase phrase = null;

                // 如果没有指定该流程对应的短语，就插入一条
                if (string.IsNullOrEmpty(step.PhraseID))
                {
                    phrase = new EnglishPhrase();
                    englishPhraseCollection.InsertOne(phrase);
                    step.PhraseID = phrase.Id.ToString();
                }

                // 如果当前流程已有对应短语，则查询出来
                else
                {
                    phrase = englishPhraseCollection.AsQueryable().Where(m => m.Id == new ObjectId(step.PhraseID)).FirstOrDefault();
                    if (phrase == default(EnglishPhrase)) throw new ApplicationException("没有找到对应的短语、语法信息");
                }

                // 获取短语、单词内容/短语、单词释义 默认获取最后一条，只要不确认，以前的就不保存
                var phraseContent = step.CurrentSetpContent.Last();

                switch (step.CurrentStep)
                {
                    case 1: // 记录短语
                        #region 记录短语                        
                        step.CurrentStep = 2;
                        step.IsConfirmSave = 0;
                        step.CurrentSetpContent.Clear();
                        UpdateStudyPhraseStep(step);
                        englishPhraseCollection.UpdateOne(m => m.Id == phrase.Id, Builders<EnglishPhrase>.Update.Set(m => m.PhraseText, phraseContent));
                        #endregion
                        break;
                    case 2: // 记录释义
                        #region 记录释义
                        step.CurrentStep = 3;
                        step.IsConfirmSave = 0;
                        step.CurrentSetpContent.Clear();
                        UpdateStudyPhraseStep(step);
                        englishPhraseCollection.UpdateOne(m => m.Id == phrase.Id, Builders<EnglishPhrase>.Update.Set(m => m.Paraphrase, phraseContent));
                        #endregion
                        break;

                    case 3: // 记录例句
                        #region 记录例句
                        var exampleSentences = step.CurrentSetpContent.ToArray();
                        step.CurrentStep = 0;
                        step.PhraseID = null;
                        step.IsConfirmSave = 0;
                        step.CurrentSetpContent.Clear();
                        UpdateStudyPhraseStep(step);
                        englishPhraseCollection.UpdateOne(m => m.Id == phrase.Id, Builders<EnglishPhrase>.Update.Set(m => m.ExampleSentences, exampleSentences));
                        #endregion
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                InsertError(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 以异步的方式查找一个随机的英语语法、短语
        /// </summary>
        /// <returns>查找结果</returns>
        public static async Task<string> FindRandomEnglishPhrase()
        {
            return await Task.Run(() =>
            {
                try
                {
                    // 还是用skip和limit来做算了，损失一些性能，提高一些随机性
                    Random random = new Random();

                    var phraseCount = (int)englishPhraseCollection.Count(m => true);

                    var phrases = englishPhraseCollection.Aggregate().ToList();

                    if (phrases.Count > 0)
                    {
                        var phrase = phrases[random.Next(0, phrases.Count)];
                        return "--------------------------------------\r\n{0}\r\n--------------------------------------\r\n{1}\r\n--------------------------------------\r\n{2}".FormatString(
                                phrase.PhraseText,
                                phrase.Paraphrase,
                                string.Join("\r\n\r\n", phrase.ExampleSentences)
                            );
                    }

                    return "没有找到短语、语法信息";
                }
                catch (Exception ex)
                {
                    InsertError(ex);
                    return "没有找到短语、语法信息";
                }
            });
        }
    }
}
