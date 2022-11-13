using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingApp.UI.Models.MongoDbModels
{
    public class Message : MongoDbEntity
    {
        [BsonElement("text"), BsonRepresentation(BsonType.String)]
        public string Text { get; set; }
        [BsonElement("roomId")]
        public string RoomId { get; set; }
        [BsonElement("userId")]
        public string UserId { get; set; }
    }
}
