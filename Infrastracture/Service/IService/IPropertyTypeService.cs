using Core.CommendDto;
using Core.Filter;
using Core.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService;

public interface IPropertyTypeService
{
    Task<List<PropertyTypeDto>> GetPropertyTypeDto(PropertyTypeFilter filter);
    Task<string> CreatePropertyTypeDto(CreatePropertyTypeDto propertyTypeDto, string propertyId);
}
