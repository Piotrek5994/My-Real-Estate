using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace Core.CommendDto;

public class CreateUserDto
{
    [Required]
    [JsonProperty(Required = Required.Always)]
    public string FirstName { get; set; }
    [Required]
    [JsonProperty(Required = Required.Always)]
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string PESEL { get; set; }

    [Required]
    [JsonProperty(Required = Required.Always)]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required]
    [JsonProperty(Required = Required.Always)]
    [RegularExpression(
        @"^[A-Z](?=.*\d{4,})(?=.*[!@#$%^&*()\-_=+{};:,<.>]).+$",
        ErrorMessage = "Password must start with a capital letter, contain at least 4 digits and one special character."
    )]
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public class CreateUserDtoExample : IExamplesProvider<CreateUserDto>
    {
        public CreateUserDto GetExamples()
        {
            return new CreateUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Gender = "Male",
                PESEL = "12345678901",
                Email = "john.doe@example.com",
                Password = "Password1234!",
                PhoneNumber = "123456789"
            };
        }
    }
}
