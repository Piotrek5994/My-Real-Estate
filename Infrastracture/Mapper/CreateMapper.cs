using AutoMapper;
using Core.Commend.Create;
using Core.CommendDto;
using Infrastracture.Helper;

namespace Infrastracture.Mapper;

public class CreateMapper : Profile
{
    public CreateMapper()
    {
        CreateMap<CreateUserDto, CreateUser>()
                .BeforeMap((src,dest) =>
                {
                    src.Password = PasswordHasher.HashPassword(src.Password).ToString();
                })
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
