namespace PersonalAPIService.Entity
{
    using MongoDB.Bson;
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public sealed class CollectedArtical
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonDefaultValue(defaultValue: "标题")]
        [BsonElement(elementName: "title")]
        public string ArticleTitle { get; set; }

        [BsonDefaultValue(defaultValue: "副标题")]
        [BsonElement(elementName: "sbtitle")]
        public string ArticleSubtitle { get; set; }

        [BsonDefaultValue(defaultValue: "文章链接地址")]
        [BsonElement(elementName: "url")]
        public string ArticleUrl { get; set; }

        [BsonDefaultValue(defaultValue: "文章类型")]
        [BsonElement(elementName: "type")]
        public int ArticleType { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement(elementName: "create_time")]
        public DateTime ArticleCreateTime { get; set; } = DateTime.Now;

        [BsonDefaultValue(defaultValue: "微信openid")]
        [BsonElement(elementName: "openid")]
        public string OpenId { get; set; }

        [BsonDefaultValue(defaultValue:"")]
        [BsonElement(elementName:"html")]
        public string HtmlContent { get; set; }
    }
}
