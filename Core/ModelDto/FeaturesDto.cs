using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ModelDto;

public class FeaturesDto
{
    public string Id { get; set; }
    public string FeatureName { get; set; }
    public string UserId { get; set; }
}
