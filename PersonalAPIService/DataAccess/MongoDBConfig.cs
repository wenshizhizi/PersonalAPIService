
namespace PersonalAPIService.DataAccess
{
    using Entity;
    using MongoDB.Driver;

    partial class MongodbHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static readonly string _connectionString = "mongodb://homeserver.iicp.net:9528";

        /// <summary>
        /// 数据库
        /// </summary>
        private static readonly string _dataBase = "notedata";

        /// <summary>
        /// mongodb api 的客户端
        /// </summary>
        protected static readonly MongoClient client = new MongoClient(_connectionString);

        /// <summary>
        /// 单词集合
        /// </summary>
        protected static readonly IMongoCollection<EnglishWord> englishWordsCollection = client.GetDatabase(_dataBase).GetCollection<EnglishWord>("words");

        /// <summary>
        /// 文章集合
        /// </summary>
        protected static readonly IMongoCollection<CollectedArtical> articleCollection = client.GetDatabase(_dataBase).GetCollection<CollectedArtical>("articles");

        /// <summary>
        /// 客户配置集合
        /// </summary>
        protected static readonly IMongoCollection<CustomerConfig> customerConfigCollection = client.GetDatabase(_dataBase).GetCollection<CustomerConfig>("customer_config");

        /// <summary>
        /// 日志集合
        /// </summary>
        protected static readonly IMongoCollection<LogInfo> logCollection = client.GetDatabase(_dataBase).GetCollection<LogInfo>("log_info");

        /// <summary>
        /// 单词、短语采集步骤集合
        /// </summary>
        protected static readonly IMongoCollection<StudyPhraseStep> phraseStepCollection = client.GetDatabase(_dataBase).GetCollection<StudyPhraseStep>("phrase_step");

        /// <summary>
        /// 单词短语集合
        /// </summary>
        protected static readonly IMongoCollection<EnglishPhrase> englishPhraseCollection = client.GetDatabase(_dataBase).GetCollection<EnglishPhrase>("phrases");

        /// <summary>
        /// 上一次生成的随机单词集合
        /// </summary>
        protected static readonly IMongoCollection<LastRandomWords> lastRandomWordsCollection = client.GetDatabase(_dataBase).GetCollection<LastRandomWords>("last_random_words");
    }
}
