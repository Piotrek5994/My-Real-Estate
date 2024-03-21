using Core.CommandDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService
{
    public interface IFeaturesService
    {
        Task<List<string>> CreateFeaturesDto(List<CreateFeaturesDto> featuresDto, string propertyId);
    }
}
