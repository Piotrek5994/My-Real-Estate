using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("first_Name")]
    public string FirstName { get; set; }
    [BsonElement("last_Name")]
    public string LastName { get; set; }
    [BsonElement("gender")]
    public string Gender { get; set; }
    [BsonElement("pesel")]
    public string PESEL { get; set; }
    [BsonElement("role")]
    public string Role { get; set; }
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("password")]
    public string Password { get; set; }
    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; }
    [BsonElement("properties")]
    public List<string>? Properties { get; set; }
    [BsonElement("payments")]
    public List<string>? Payments { get; set; }
}
