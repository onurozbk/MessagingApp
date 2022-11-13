using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingApp.UI.Models.MongoDbModels
{
    public class Room : MongoDbEntity
    {
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
    }
}
