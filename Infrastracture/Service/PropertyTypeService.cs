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

public class PropertyTypeService : IPropertyTypeService
{
    private readonly IPropertyTypeRepository _propertyTypeRepository;
    private readonly IMapper _mapper;

    public PropertyTypeService(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
    {
        _propertyTypeRepository = propertyTypeRepository;
        _mapper = mapper;
    }
    public async Task<PropertyTypeDto> GetPropertyTypeDto(PropertyTypeFilter filter)
    {
        return null;
    }
    public async Task<string> CreatePropertyTypeDto(CreatePropertyTypeDto propertyTypeDto, string propertyId)
    {
        CreatePropertyType propertyType = _mapper.Map<CreatePropertyType>(propertyTypeDto);
        var result = await _propertyTypeRepository.CreatePropertyType(propertyType,propertyId);
        return result;
    }
}
