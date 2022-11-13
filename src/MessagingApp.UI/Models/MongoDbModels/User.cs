using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingApp.UI.Models.MongoDbModels
{
    public class User : MongoDbEntity
    {
        [BsonElement("nickName"), BsonRepresentation(BsonType.String)]
        public string NickName { get; set; }
    }
}
