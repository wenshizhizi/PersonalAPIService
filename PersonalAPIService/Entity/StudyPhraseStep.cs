namespace PersonalAPIService.Entity
{
    using MongoDB.Bson;
    using System;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    public sealed class StudyPhraseStep
    {
        /// <summary>
        /// ID
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 微信openid
        /// </summary>
        [BsonDefaultValue(defaultValue: "微信openid")]
        [BsonElement(elementName: "openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 当前步骤对应的短语ID
        /// </summary>
        [BsonElement(elementName: "phraseid")]
        public string PhraseID { get; set; }

        /// <summary>
        /// 当前步骤
        /// <para>0:完成</para>
        /// <para>1:接受短语</para>
        /// <para>2:接受释义</para>
        /// <para>3:接受例句</para>
        /// </summary>
        [BsonDefaultValue(defaultValue: 0)]
        [BsonElement(elementName: "current_step")]
        public int CurrentStep { get; set; } = 0;

        /// <summary>
        /// 是否保存当前操作，1为保存并跳到下一步
        /// </summary>
        [BsonElement(elementName: "now_step_chose")]
        public int IsConfirmSave { get; set; } = 0;

        /// <summary>
        /// 当前步骤内容
        /// </summary>
        [BsonElement(elementName: "current_content")]
        public List<string> CurrentSetpContent { get; set; } = new List<string>();
    }

    public sealed class EnglishPhrase
    {
        /// <summary>
        /// ID
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 语法、短语文本
        /// </summary>
        [BsonElement(elementName: "phrase_text")]
        public string PhraseText { get; set; } = "";

        /// <summary>
        /// 释义
        /// </summary>
        [BsonElement(elementName: "paraphrase")]
        public string Paraphrase { get; set; } = "";

        /// <summary>
        /// 例句
        /// </summary>
        [BsonElement(elementName: "expsen")]
        public string[] ExampleSentences { get; set; } = new string[] { };

        /// <summary>
        /// 创建时间
        /// </summary>
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
