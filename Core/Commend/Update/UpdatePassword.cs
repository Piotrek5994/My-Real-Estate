using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commend.Update
{
    public class UpdatePassword
    {
        [Required]
        public string userId {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(
            @"^[A-Z](?=.*\d{4,})(?=.*[!@#$%^&*()\-_=+{};:,<.>]).+$",
            ErrorMessage = "Password must start with a capital letter, contain at least 4 digits and one special character."
        )]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(
            @"^[A-Z](?=.*\d{4,})(?=.*[!@#$%^&*()\-_=+{};:,<.>]).+$",
            ErrorMessage = "Password must start with a capital letter, contain at least 4 digits and one special character."
        )]
        public string NewPassword { get; set; }
    }
}
