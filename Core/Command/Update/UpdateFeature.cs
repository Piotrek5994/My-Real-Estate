﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Command.Update;

public class UpdateFeature
{
    [BsonElement("features_name")]
    public string FeatureName { get; set; }
}
