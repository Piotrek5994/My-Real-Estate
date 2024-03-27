using AutoMapper;
using Core.Model;
using Core.ModelDto;
using Infrastracture.ModelDto;

namespace Infrastracture.Mapper;

public class GetMapper : Profile
{
    public GetMapper()
    {
        CreateMap<User, UserDto>()
                .AfterMap((src, dest) =>
                {
                    src.Id = dest.Id;
                    src.FirstName = dest.FirstName;
                    src.LastName = dest.LastName;
                    src.Gender = dest.Gender;
                    src.PESEL = dest.PESEL;
                    src.Role = dest.Role;
                    src.Email = dest.Email;
                    src.Password = dest.Password;
                    src.Properties = dest.Properties;
                    src.Payments = dest.Payments;
                });
        CreateMap<Property, PropertyDto>()
                .AfterMap((src, dest) =>
                {
                    src.Id = dest.Id;
                    src.Name = dest.Name;
                    src.Description = dest.Description;
                    src.Price = dest.Price;
                    src.Size = dest.Size;
                    src.NumberOfRooms = dest.NumberOfRooms;
                    src.NumberOfPeople = dest.NumberOfPeople;
                    src.RentStart = dest.RentStart;
                    src.RentEnd = dest.RentEnd;
                })
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => Convert.ToInt32(src.State)));
        CreateMap<PropertyType, PropertyTypeDto>()
                .AfterMap((src, dest) =>
                {
                    src.Id = dest.Id;
                    src.PropertyTypeName = dest.PropertyTypeName;
                    src.PropertyId = dest.PropertyId;
                });
        CreateMap<Features, FeaturesDto>()
                .AfterMap((src, dest) =>
                {
                    src.Id = dest.Id;
                    src.FeatureName = dest.FeatureName;
                    src.PropertyId = dest.PropertyId;
                });
    }
}
