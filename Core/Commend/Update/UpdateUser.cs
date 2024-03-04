using Core.CommendDto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commend.Update
{
    public class UpdateUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? PESEL { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class UpdateUserExample : IExamplesProvider<UpdateUser>
    {
        public UpdateUser GetExamples()
        {
            return new UpdateUser
            {
                FirstName = "",
                LastName = "",
                Gender = "",
                PESEL = "",
                Email = "",
                Password = "",
                PhoneNumber = ""
            };
        }
    }
}
