using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Features
    {
        public int Id { get; set; }
        public string Feature_Name { get; set; } = string.Empty;
        public Property property { get; set; }
    }
}
