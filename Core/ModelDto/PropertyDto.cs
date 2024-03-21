using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ModelDto;

public class PropertyDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Size { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime? RentStart { get; set; }
    public DateTime? RentEnd { get; set; }
    public int State { get; set; }
    public string UserId { get; set; }
    public List<string>? Photos { get; set; }
    public List<string>? Features { get; set; }
}
