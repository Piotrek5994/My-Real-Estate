using AutoMapper;
using Core.Commend.Create;
using Core.Commend.Update;
using Core.CommendDto.Create;
using Core.CommendDto.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Mapper
{
    public class UpdateMapper : Profile
    {
        public UpdateMapper()
        {
            CreateMap<UpdateUserDto, UpdateUser>()
                   .AfterMap((src, dest) =>
                   {
                       src.FirstName = dest.FirstName;
                       src.LastName = dest.LastName;
                       src.Gender = dest.Gender;
                       src.PESEL = dest.PESEL;
                       src.Email = dest.Email;
                       src.Password = dest.Password;
                       src.PhoneNumber = dest.PhoneNumber;
                       src.Properties = dest.Properties;
                       src.Payments = dest.Payments;
                   });
        }
    }
}
