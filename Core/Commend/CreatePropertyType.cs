using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commend
{
    public class CreatePropertyType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        [BsonElement("property_type_name")]
        public string PropertyTypeName { get; set; } = string.Empty;
        [BsonRequired]
        [BsonElement("property_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PropertyId { get; set; } //FK
    }
}
