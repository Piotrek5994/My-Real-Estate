using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class PropertyAddress
    {
        public int PropAddId { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street_Name { get; set; } = string.Empty;
        public string Street_Number { get; set; } = string.Empty;
    }
}
