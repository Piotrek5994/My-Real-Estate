using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Property_Type
    {
        public int Id { get; set; }
        public string Property_Type_Name { get; set; } = string.Empty;
        public int PropertyId { get; set; }
        public Property property { get; set; }
    }
}
