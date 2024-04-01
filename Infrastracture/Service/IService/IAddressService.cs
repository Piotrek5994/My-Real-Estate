using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Command.Update;
using Core.CommandDto;
using Core.Commend.Update;
using Core.Filter;
using Core.ModelDto;

namespace Infrastracture.Service.IService;

public interface IAddressService
{
    Task<List<AddressDto>> GetAddressDto(AddressFilter filter);
    Task<string> CreateAddressDto(CreateAddressDto addressDto);
    Task<bool> UpdateAddress(UpdateAddress address, string addressId);
    Task<bool> DeleteAddress(string propertyId);
}
