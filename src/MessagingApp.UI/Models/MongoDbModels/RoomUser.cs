using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingApp.UI.Models.MongoDbModels
{
    public class RoomUser : MongoDbEntity
    {
        [BsonElement("roomId")]
        public string UserId { get; set; }
        [BsonElement("userId")]
        public string RoomId { get; set; }
    }
}
