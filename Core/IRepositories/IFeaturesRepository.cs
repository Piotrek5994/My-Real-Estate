using Core.Commend.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories;

public interface IFeaturesRepository
{
    Task<List<string>> CreateFeatures(List<CreateFeatures> features, string propertyId);
}
