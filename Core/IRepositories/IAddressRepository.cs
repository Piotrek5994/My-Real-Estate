using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Command.Update;
using Core.Commend.Create;
using Core.Filter;
using Core.Model;

namespace Core.IRepositories;

public interface IAddressRepository
{
    Task<List<Address>> GetAddress(AddressFilter filter);
    Task<string> CreateAddressUser(CreateAddress address);
    Task<string> CreateAddressProperty(CreateAddress address);
    Task<bool> UpdateAddress(UpdateAddress address, string addressId);
    Task<bool> DeleteAddress(string propertyId);
}
