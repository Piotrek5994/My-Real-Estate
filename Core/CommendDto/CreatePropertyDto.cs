﻿using Core.Validation;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.CommendDto
{
    public class CreatePropertyDto
    {
        [Required]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
        public string Description { get; set; }
        [DecimalPrecision(2)]
        public decimal Price { get; set; }
        [Required]
        [JsonProperty(Required = Required.Always)]
        public int Size { get; set; }
        [Required]
        [JsonProperty(Required = Required.Always)]
        public int NumberOfRooms { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime? RentStart { get; set; }
        public DateTime? RentEnd { get; set; }
        [Required]
        [JsonProperty(Required = Required.Always)]
        public int State { get; set; }
        [Required]
        [JsonProperty(Required = Required.Always)]
        public string UserId { get; set; }
        public class CreatePropertyDtoExample : IExamplesProvider<CreatePropertyDto>
        {
            public CreatePropertyDto GetExamples()
            {
                return new CreatePropertyDto
                {
                    Name = "Ocean View Apartment",
                    Description = "A luxurious apartment with 3 bedrooms overlooking the ocean. Features include a fully equipped kitchen, spacious living room, and access to a private beach.",
                    Price = 1500.00m,
                    Size = 120,
                    NumberOfRooms = 3,
                    NumberOfPeople = 6,
                    RentStart = new DateTime(2024, 6, 1),
                    RentEnd = new DateTime(2024, 8, 31),
                    State = 0,
                    UserId = "user123"
                };
            }
        }
    }
}