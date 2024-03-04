using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commend.Update
{
    public class UpdateUser
    {
        [BsonElement("first_Name")]
        public string FirstName { get; set; } = string.Empty;
        [BsonElement("last_Name")]
        public string LastName { get; set; } = string.Empty;
        [BsonElement("gender")]
        public string Gender { get; set; } = string.Empty;
        [BsonElement("pesel")]
        public string PESEL { get; set; } = string.Empty;
        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
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
