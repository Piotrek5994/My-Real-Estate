﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Zip_Code { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street_Name { get; set; } = string.Empty;
        public string Street_Number { get; set; } = string.Empty;
        public int State { get; set; }
        public int AdressId { get; set; } //FK
        public Address address { get; set; }
        public int UserId { get; set; } //FK
        public User user { get; set; }
    }
}