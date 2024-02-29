using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Size { get; set; }
        public short Number_of_rooms { get; set; }
        public short? Number_of_people { get; set; }
        public DateTime? Rent_Start { get; set; }
        public DateTime? Rent_End { get; set; }
        public int State { get; set; }
        public int UserId { get; set; }
    }
}
