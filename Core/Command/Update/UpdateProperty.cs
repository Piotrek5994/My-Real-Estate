using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;
using Core.Validation;

namespace Core.Commend.Update;

public class UpdateProperty
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    [DecimalPrecision(2)]
    public decimal? Price { get; set; }
    public int? Size { get; set; }
    public int? NumberOfRooms { get; set; }
    public int? NumberOfPeople { get; set; }
    public DateTime? RentStart { get; set; }
    public DateTime? RentEnd { get; set; }
    public int? State { get; set; }
}
public class UpdatePropertyExample : IExamplesProvider<UpdateProperty>
{
    public UpdateProperty GetExamples()
    {
        return new UpdateProperty
        {
            Name = "",
            Description = "",
            Price = null,
            Size = null,
            NumberOfRooms = null,
            NumberOfPeople = null,
            RentStart = null,
            RentEnd = null,
            State = null
        };
    }
}
