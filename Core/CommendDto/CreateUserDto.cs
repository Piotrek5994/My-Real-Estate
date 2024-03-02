﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CommendDto
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Gender { get; set; }
        public string PESEL { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
    }
}
