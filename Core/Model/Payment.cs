using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public User user { get; set; }
    }
}
