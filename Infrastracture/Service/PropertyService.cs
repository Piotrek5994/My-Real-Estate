using AutoMapper;
using Core.Commend.Create;
using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Core.IRepositories;
using Core.ModelDto;
using Infrastracture.Service.IService;

namespace Infrastracture.Service;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IPropertyTypeRepository _typeRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IFeaturesRepository _featuresRepository;
    private readonly IMapper _mapper;

    public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IAddressRepository addressRepository, IPropertyTypeRepository typeRepository, IPhotoRepository photoRepository, IFeaturesRepository featuresRepository)
    {
        _propertyRepository = propertyRepository;
        _addressRepository = addressRepository;
        _typeRepository = typeRepository;
        _photoRepository = photoRepository;
        _featuresRepository = featuresRepository;
        _mapper = mapper;
    }
    public async Task<List<PropertyDto>> GetPropertyDto(PropertyFilter filter)
    {
        var result = await _propertyRepository.GetProperty(filter);
        var propertyDto = _mapper.Map<List<PropertyDto>>(result);
        return propertyDto;
    }
    public async Task<string> CreatePropertyDto(CreatePropertyDto propertyDto)
    {
        CreateProperty property = _mapper.Map<CreateProperty>(propertyDto);
        var result = await _propertyRepository.CreateProperty(property);
        return result;
    }
    public async Task<bool> UpdateProperty(UpdateProperty property, string propertyId) => await _propertyRepository.UpdateProperty(property, propertyId);
    public async Task<bool> DeleteProperty(string propertyId)
    {
       var result = await _propertyRepository.DeleteProperty(propertyId);
       return result;
    }
}
