﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Filter;

public class PropertyFilter
{
    public string? Id { get; set; }
    public int Limit { get; set; } = 10;
    public int Page { get; set; } = 1;
    public string SortBy { get; set; } = "name";
    public bool SortDescending { get; set; } = false;
    public string? UserId { get; set; }
}
