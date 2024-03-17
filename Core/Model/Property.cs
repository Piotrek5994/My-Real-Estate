using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model;

public class Property
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    [BsonElement("size")]
    public int Size { get; set; }
    [BsonElement("number_of_rooms")]
    public int NumberOfRooms { get; set; }
    [BsonElement("number_of_people")]
    public int NumberOfPeople { get; set; }
    [BsonElement("rent_start")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? RentStart { get; set; }
    [BsonElement("rent_end")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? RentEnd { get; set; }
    [BsonElement("state")]
    public string State { get; set; }
    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } //FK
    [BsonElement("photos")]
    public List<string>? Photos { get; set; }
    [BsonElement("features")]
    public List<string>? Features { get; set; }
}
