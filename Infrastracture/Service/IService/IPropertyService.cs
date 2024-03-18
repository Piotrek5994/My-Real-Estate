using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Core.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService;

public interface IPropertyService
{
    Task<List<PropertyDto>> GetPropertyDto(PropertyFilter filter);
    Task<string> CreatePropertyDto(CreatePropertyDto propertyDto);
    Task<bool> UpdateProperty(UpdateProperty property, string propertyId);
    Task<bool> DeleteProperty(string propertyId);
}
