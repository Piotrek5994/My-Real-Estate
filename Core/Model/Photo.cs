using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model;

public class Photo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("photo_src")]
    public string PhotoSrc { get; set; }
    [BsonElement("property_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string PropertyId { get; set; } //FK
}
