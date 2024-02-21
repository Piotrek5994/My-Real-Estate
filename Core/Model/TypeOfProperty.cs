using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class TypeOfProperty
    {
        public int TypeId { get; set; }
        public string House { get; set; } = string.Empty;
        public string Apartament { get; set; } = string.Empty;
        public string Garage { get; set; } = string.Empty;
    }
}
