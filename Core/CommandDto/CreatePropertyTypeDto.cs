using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CommendDto
{
    public class CreatePropertyTypeDto
    {
        [Required]
        [JsonProperty(Required = Required.Always)]
        [RegularExpression("^(Garage|Room|Apartment|garage|room|apartment)$", ErrorMessage = "Invalid property name : Garage, Room, Apartment, garage, room, apartment")]
        public string PropertyTypeName { get; set; } = string.Empty;
    }
    public class CreatePropertyTypeDtoExample : IExamplesProvider<CreatePropertyTypeDto>
    {
        public CreatePropertyTypeDto GetExamples()
        {
            return new CreatePropertyTypeDto
            {
                PropertyTypeName = "Apartments"
            };
        }
    }
}
