using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Filters;

namespace Core.Command.Update;

public class UpdateFeature
{
    [BsonElement("features_name")]
    public string FeatureName { get; set; }
}
public class UpdateFeatureExample : IExamplesProvider<UpdateFeature>
{
    public UpdateFeature GetExamples()
    {
        return new UpdateFeature
        {
            FeatureName = "Tv"
        };
    }
}
