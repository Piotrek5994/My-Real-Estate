using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Zip_Code { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street_Name { get; set; } = string.Empty;
        public string Street_Number { get; set; } = string.Empty;
        public int State { get; set; }
        public string AdressId { get; set; } //FK
        public string UserId { get; set; } //FK
    }
}
