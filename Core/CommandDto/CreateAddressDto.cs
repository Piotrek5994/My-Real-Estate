using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Core.CommendDto;
using Swashbuckle.AspNetCore.Filters;
using Core.Model;

namespace Core.CommandDto;

public class CreateAddressDto
{
    [Required]
    [JsonProperty(Required = Required.Always)]
    public string Country { get; set; }

    [Required]
    [JsonProperty(Required = Required.Always)]
    public string Region { get; set; }

    [Required]
    [JsonProperty(Required = Required.Always)]
    public string ZipCode { get; set; }

    [Required]
    [JsonProperty(Required = Required.Always)]
    public string City { get; set; }

    [Required]
    [JsonProperty(Required = Required.Always)]
    public string StreetName { get; set; }

    [Required]
    [JsonProperty(Required = Required.Always)]
    public string StreetNumber { get; set; }

    [JsonProperty(Required = Required.Default)]
    public string? PropertyId { get; set; } //FK

    [JsonProperty(Required = Required.Default)]
    public string? UserId { get; set; } //FK

    public class CreateAddressDtoExample : IExamplesProvider<CreateAddressDto>
    {
        public CreateAddressDto GetExamples()
        {
            return new CreateAddressDto()
            {
                Country = "Poland",
                Region = "Małopolskie",
                ZipCode = "31-550",
                City = "Kraków",
                StreetName = "Kolorowa",
                StreetNumber = "12",
                PropertyId = "65f82029010a574fe727cbc4"
            };
        }
    }
}