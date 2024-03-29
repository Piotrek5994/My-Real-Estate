﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Filter;

public class FeaturesFilter
{
    public string? Id { get; set; }
    public int Limit { get; set; } = 10;
    public int Page { get; set; } = 1;
    public string SortBy { get; set; } = "features_name";
    public bool SortDescending { get; set; } = false;
    public string? PropertyId { get; set; }
}
