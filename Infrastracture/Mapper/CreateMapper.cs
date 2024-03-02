using AutoMapper;
using Core.Commend;
using Core.CommendDto;

namespace Infrastracture.Mapper
{
    public class CreateMapper : Profile
    {
        public CreateMapper()
        {
            CreateMap<CreateUserDto, CreateUser>()
                    .AfterMap((src, dest) =>
                     {
                         src.FirstName = dest.FirstName;
                         src.LastName = dest.LastName;
                         src.Gender = dest.Gender;
                         src.PESEL = dest.PESEL;
                         src.Email = dest.Email;
                         src.Password = dest.Password;
                         src.PhoneNumber = dest.PhoneNumber;
                     });
        }
    }
}
