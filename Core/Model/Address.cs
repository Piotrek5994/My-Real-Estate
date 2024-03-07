using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model;

public class Address
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("country")]
    public string Country { get; set; }
    [BsonElement("region")]
    public string Region { get; set; }
    [BsonElement("zip_code")]
    public string ZipCode { get; set; }
    [BsonElement("city")]
    public string City { get; set; }
    [BsonElement("street_name")]
    public string StreetName { get; set; }
    [BsonElement("street_number")]
    public string StreetNumber { get; set; }
    [BsonElement("state")]
    public int State { get; set; }
    [BsonElement("property_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string PropertyId { get; set; } //FK
    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } //FK
}
