using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commend
{
    public class CreateUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        [BsonElement("first_Name")]
        public string FirstName { get; set; } = string.Empty;
        [BsonRequired]
        [BsonElement("last_Name")]
        public string LastName { get; set; } = string.Empty;
        [BsonElement("gender")]
        public string Gender { get; set; } = string.Empty;
        [BsonRequired]
        [BsonElement("pesel")]
        public string PESEL { get; set; } = string.Empty;
        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;
        [BsonRequired]
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
        [BsonRequired]
        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;
        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [BsonElement("properties")]
        public List<string>? Properties { get; set; }
        [BsonElement("payments")]
        public List<string>? Payments { get; set; }
    }
}
