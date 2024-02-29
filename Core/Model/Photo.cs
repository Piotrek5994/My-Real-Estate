﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoSrc { get; set; } = string.Empty;
        public int PropertyId { get; set; }
        public Property property { get; set; }
    }
}