﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Payment
    {
        public string Id { get; set; }
        public string Person_Renting { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; } //FK
    }
}
