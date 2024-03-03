using AutoMapper;
using Core.Model;
using Infrastracture.ModelDto;

namespace Infrastracture.Mapper
{
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
        }
    }
}
