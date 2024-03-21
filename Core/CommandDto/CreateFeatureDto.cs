using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.CommandDto
{
    public class CreateFeatureDto
    {
        [Required]
        [JsonProperty(Required = Required.Always)]
        public string FeatureName { get; set; }
    }
}
