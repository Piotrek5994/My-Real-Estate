using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model;

public class PropertyType
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("property_type_name")]
    public string PropertyTypeName { get; set; }
    [BsonElement("property_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string PropertyId { get; set; } //FK
}
