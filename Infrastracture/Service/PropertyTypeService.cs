using AutoMapper;
using Core.IRepositories;
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
}
