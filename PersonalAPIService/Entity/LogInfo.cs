namespace PersonalAPIService.Entity
{
    using MongoDB.Bson;
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public sealed class LogInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonDefaultValue(defaultValue: "日志类型")]
        [BsonElement(elementName: "log_level")]
        public string LogLevel { get; set; }

        [BsonDefaultValue(defaultValue: "日志内容")]
        [BsonElement(elementName: "log_content")]
        public string LogContent { get; set; }

        [BsonDefaultValue(defaultValue: "暂无")]
        [BsonElement(elementName: "other_info_detail")]
        public string OtherInfo { get; set; } = "暂无";

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement(elementName: "create_time")]
        public DateTime LogTime { get; set; } = DateTime.Now;        
    }
}
