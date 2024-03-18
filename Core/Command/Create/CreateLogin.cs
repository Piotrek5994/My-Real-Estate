using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Commend.Create;

public class CreateLogin
{
    [Required]
    [JsonProperty(Required = Required.Always)]
    public string Email { get; set; }
    [Required]
    [JsonProperty(Required = Required.Always)]
    public string Password { get; set; }
}
