using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Property
    {
        public int PropId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int Number_of_rooms { get; set; }
        //public List<> Room_photo { get; set; }
        //public List<> Room_equipment { get; set; }
        public int PropAddId { get; set; }
        //public PropertyAddress propertyAddress { get; set; }
    }
}
