﻿using Core.Commend.Create;
using Core.Commend.Update;
using Core.Filter;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories;

public interface IPropertyRepository
{
    Task<List<Property>> GetProperty(PropertyFilter filter);
    Task<string> CreateProperty(CreateProperty property);
    Task<bool> UpdateProperty(UpdateProperty updateProperty, string propertyId);
    Task<bool> DeleteProperty(string propertyId);
}
