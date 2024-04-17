using AutoMapper;
using Core.IRepositories;
using Infrastracture.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Commend.Create;
using Core.Filter;
using Core.ModelDto;
using Core.CommandDto;
using Infrastracture.Repositories;
using Core.Command.Update;

namespace Infrastracture.Service;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddressService(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task<List<AddressDto>> GetAddressDto(AddressFilter filter)
    {
        var result = await _addressRepository.GetAddress(filter);
        var addressDto = _mapper.Map<List<AddressDto>>(result);
        return addressDto;
    }

    public async Task<string> CreateAddressDto(CreateAddressDto addressDto)
    {
        switch (addressDto.PropertyId, addressDto.UserId)
        {
            case (null, null):
                return "Address can't have both UserId and PropertyId set to null";
            case (not null,not null):
                return "Address can't have both UserId and PropertyId";
        }
        CreateAddress address = _mapper.Map<CreateAddress>(addressDto);
        address.State = address.PropertyId != null ? 1 : 0;
        string result = address.State switch
        {
            int when address.State == 0 => await _addressRepository.CreateAddressUser(address),
            int when address.State == 1 => await _addressRepository.CreateAddressProperty(address),
            _ => "Invalid state!"
        };
        return result;
    }

    public async Task<bool> UpdateAddress(UpdateAddress updateAddress , string addressId) => await _addressRepository.UpdateAddress(updateAddress, addressId);
    public async Task<bool> DeleteAddress(string addressId) => await _addressRepository.DeleteAddress(addressId);

}
