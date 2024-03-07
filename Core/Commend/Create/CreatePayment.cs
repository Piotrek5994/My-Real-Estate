using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commend.Create;

public class CreatePayment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("person_renting")]
    public string PersonRenting { get; set; } = string.Empty;
    [BsonElement("amount")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Amount { get; set; }
    [BsonElement("date")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? Date { get; set; }
    [BsonRequired]
    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } //FK
}
