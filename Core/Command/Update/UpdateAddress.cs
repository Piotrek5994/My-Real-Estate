using Core.Commend.Update;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Command.Update;

public class UpdateAddress
{
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? StreetName { get; set; }
    public string? StreetNumber { get; set; }
}

public class UpdateAddressExample : IExamplesProvider<UpdateAddress>
{
    public UpdateAddress GetExamples()
    {
        return new UpdateAddress()
        {
            Country = "USA",
            Region = "Florida",
            ZipCode = "33101",
            City = "Miami",
            StreetName = "Lincoln Road",
            StreetNumber = "206"
        };
    }
}
