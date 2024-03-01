using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commend
{
    public class CreateAddress
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("country")]
        public string Country { get; set; } = string.Empty;
        [BsonElement("region")]
        public string Region { get; set; } = string.Empty;
        [BsonElement("zip_code")]
        public string ZipCode { get; set; } = string.Empty;
        [BsonElement("city")]
        public string City { get; set; } = string.Empty;
        [BsonElement("street_name")]
        public string StreetName { get; set; } = string.Empty;
        [BsonElement("street_number")]
        public string StreetNumber { get; set; } = string.Empty;
        [BsonElement("state")]
        public int State { get; set; }
        [BsonElement("property_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PropertyId { get; set; } = string.Empty; //FK
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty; //FK
    }
}
