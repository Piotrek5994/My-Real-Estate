using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace Core.CommandDto;

public class CreateFeaturesDto
{
    [Required]
    [JsonProperty(Required = Required.Always)]
    public string FeatureName { get; set; }
}
public class CreateFeatureDtoExample : IExamplesProvider<List<CreateFeaturesDto>>
{
    public List<CreateFeaturesDto> GetExamples()
    {
        return new List<CreateFeaturesDto>
        {
            new CreateFeaturesDto
            {
                FeatureName = "TV"
            }
        };
    }
}
