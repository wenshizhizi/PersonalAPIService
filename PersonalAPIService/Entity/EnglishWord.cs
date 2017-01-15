namespace PersonalAPIService.Entity
{
    using MongoDB.Bson;
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public sealed class EnglishWord
    {
        /// <summary>
        /// id
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 单词文本
        /// </summary>
        [BsonDefaultValue(defaultValue: "暂无")]
        [BsonElement(elementName: "WordText", Order = 0)]
        public string WordText { get; set; }

        /// <summary>
        /// 释义
        /// </summary>
        [BsonDefaultValue(defaultValue: "暂无")]
        [BsonElement(elementName: "Paraphrase", Order = 1)]
        public string Paraphrase { get; set; }
        
        /// <summary>
        /// 音标(美)
        /// </summary>
        [BsonDefaultValue(defaultValue: "暂无")]
        [BsonElement(elementName: "UsPhoneticSymbol", Order = 2)]
        public string UsPhoneticSymbol { get; set; }

        /// <summary>
        /// 音标(英)
        /// </summary>
        [BsonDefaultValue(defaultValue: "暂无")]
        [BsonElement(elementName: "UkPhoneticSymbol", Order = 3)]
        public string UkPhoneticSymbol { get; set; }

        /// <summary>
        /// 字典基本释义
        /// </summary>
        [BsonElement(elementName: "DictionaryExplainInfos", Order = 4)]
        public string[] DictionaryExplainInfos { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement(elementName: "create_time")]
        public DateTime InsertTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 排序字段
        /// </summary>
        [BsonElement(elementName: "order", Order = 5)]
        public double OrderField { get; set; } = new Random().NextDouble();
    }
}
