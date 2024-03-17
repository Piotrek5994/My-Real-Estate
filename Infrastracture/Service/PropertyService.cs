using AutoMapper;
using Core.Commend.Create;
using Core.CommendDto;
using Core.Filter;
using Core.IRepositories;
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
    public async Task<string> CreatePropertyDto(CreatePropertyDto propertyDto)
    {
        CreateProperty property = _mapper.Map<CreateProperty>(propertyDto);
        var result = await _propertyRepository.CreateProperty(property);
        return result;
    }

}
