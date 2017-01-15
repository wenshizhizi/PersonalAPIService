namespace PersonalAPIService.Entity
{
    using MongoDB.Bson;
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public sealed class CustomerConfig
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonDefaultValue(defaultValue: "当前使用项")]
        [BsonElement(elementName: "now_use_case")]
        public string NowUseCase { get; set; }
        
        [BsonDefaultValue(defaultValue: "微信openid")]
        [BsonElement(elementName: "openid")]
        public string OpenId { get; set; }
    }
}
