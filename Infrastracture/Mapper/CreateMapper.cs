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
        CreateMap<CreatePropertyDto, CreateProperty>()
                .AfterMap((src, dest) =>
                {
                    src.Name = dest.Name;
                    src.Description = dest.Description;
                    src.Price = dest.Price;
                    src.Size = dest.Size;
                    src.NumberOfPeople = dest.NumberOfPeople;
                    src.NumberOfPeople = dest.NumberOfPeople;
                    src.State = Convert.ToInt32(dest.State);
                })
                .ForMember(dest => dest.RentStart, opt => opt.MapFrom(src => DateHelper.GetDate(src.RentStart)))
                .ForMember(dest => dest.RentEnd, opt => opt.MapFrom(src => DateHelper.GetDate(src.RentEnd)));
    }
}
