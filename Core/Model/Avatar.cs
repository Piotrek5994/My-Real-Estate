using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Avatar
    {
        public string Id { get; set; }
        public string AvatarScr {  get; set; } = string.Empty;
        public string UserId { get; set; } //FK
    }
}
