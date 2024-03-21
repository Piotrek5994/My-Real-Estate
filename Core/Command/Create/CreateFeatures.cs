using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commend.Create;

public class CreateFeatures
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("features_name")]
    public string FeatureName { get; set; } = string.Empty;
    [BsonRequired]
    [BsonElement("property_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string PropertyId { get; set; }
}
