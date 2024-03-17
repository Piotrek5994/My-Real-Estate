using AutoMapper;
using Core.Commend.Create;
using Core.CommendDto;
using Core.Filter;
using Core.IRepositories;
using Core.ModelDto;
using Infrastracture.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public PropertyService(IPropertyRepository propertyRepository, IMapper mapper)
    {
        _propertyRepository = propertyRepository;
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
    public async Task<bool> DeleteProperty(string propertyId)
    {

    }

}
