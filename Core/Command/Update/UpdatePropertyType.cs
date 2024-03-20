using Core.Commend.Update;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Command.Update
{
    public class UpdatePropertyType
    {
        [RegularExpression("^(Garage|Room|Apartment|garage|room|apartment)$", ErrorMessage = "Invalid property name : Garage, Room, Apartment, garage, room, apartment")]
        public string? PropertyTypeName { get; set; }
    }
    public class UpdatePropertyTypeExample : IExamplesProvider<UpdatePropertyType>
    {
        public UpdatePropertyType GetExamples()
        {
            return new UpdatePropertyType
            {
                PropertyTypeName = ""
            };
        }
    }
}
