using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Photo
    {
        public string Id { get; set; }
        public string PhotoSrc { get; set; } = string.Empty;
        public string PropertyId { get; set; }
    }
}
