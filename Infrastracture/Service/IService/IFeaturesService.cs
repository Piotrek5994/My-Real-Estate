using Core.CommandDto;
using Core.Filter;
using Core.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService
{
    public interface IFeaturesService
    {
        Task<List<FeaturesDto>> GetFeaturesDto(FeaturesFilter filter);
        Task<List<string>> CreateFeaturesDto(List<CreateFeaturesDto> featuresDto, string propertyId);
    }
}
