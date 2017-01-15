

namespace PersonalAPIService.DataAccess
{
    using Entity;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using System;

    public partial class MongodbHelper
    {
        /// <summary>
        /// 根据openid获取采集语法、短语的步骤信息
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns>步骤信息</returns>
        public static StudyPhraseStep GetStudyPhraseStepByOpenId(string openid)
        {
            try
            {
                openid = openid.Trim().ToLower();
                var ret = phraseStepCollection.AsQueryable<StudyPhraseStep>().Where(m => m.OpenId == openid).FirstOrDefault();
                if (ret == default(StudyPhraseStep))
                {
                    var insertObject = new StudyPhraseStep
                    {
                        OpenId = openid
                    };

                    phraseStepCollection.InsertOne(insertObject);

                    return insertObject;
                }
                else return ret;
            }
            catch (Exception ex)
            {
                InsertError(ex);
                return null;
            }
        }

        /// <summary>
        /// 更新获取采集语法、短语的步骤信息
        /// </summary>
        /// <param name="step">步骤信息</param>
        public static void UpdateStudyPhraseStep(StudyPhraseStep step)
        {
            try
            {
                var definition = Builders<StudyPhraseStep>.Update.
                                                Set(m => m.CurrentStep, step.CurrentStep).
                                                Set("phraseid", step.PhraseID).
                                                Set("now_step_chose", step.IsConfirmSave).
                                                Set("current_content", step.CurrentSetpContent);

                var b = phraseStepCollection.UpdateOneAsync(m => m.Id == step.Id, definition).Result;
            }
            catch (Exception ex)
            {
                InsertError(ex);
            }
        }
    }
}
