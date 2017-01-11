namespace PersonalAPIService.Entity
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    public sealed class LastRandomWords
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonDefaultValue(defaultValue: "微信openid")]
        [BsonElement(elementName: "openid")]
        public string OpenId { get; set; }

        [BsonElement(elementName: "last_words")]
        public List<string> LastGenerateWords { get; set; } = new List<string>();
    }
}
