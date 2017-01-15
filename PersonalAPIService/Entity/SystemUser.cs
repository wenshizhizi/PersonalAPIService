namespace PersonalAPIService.Entity
{
    using MongoDB.Bson;
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public sealed class SystemUser
    {
        [BsonId]
        public ObjectId Id { get; set; }
                
        [BsonElement(elementName: "username")]
        public string UserName { get; set; }

        [BsonDefaultValue(defaultValue: "123")]
        [BsonElement(elementName: "password")]
        public string Password { get; set; }

        [BsonDefaultValue(defaultValue: false)]
        [BsonElement(elementName: "is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [BsonDefaultValue(defaultValue: "1235")]
        [BsonElement(elementName: "gesture_pwd")]
        public string GesturePwd { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement(elementName: "create_time")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
