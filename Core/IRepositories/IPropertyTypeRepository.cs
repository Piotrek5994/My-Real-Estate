using Core.Commend.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories;

public interface IPropertyTypeRepository
{
    Task<string> CreatePropertyType(CreatePropertyType propertyType, string propertyId);
}
