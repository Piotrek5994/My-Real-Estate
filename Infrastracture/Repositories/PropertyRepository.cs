using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;

namespace Infrastracture.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly MongoDbContext _context;
    private readonly ILogger _log;

    public PropertyRepository(MongoDbContext context, ILogger<PropertyRepository> log)
    {
        _context = context;
        _log = log;
    }
    public async Task<Property> GetProperty(PropertyFilter filter)
    {
        return null;
    }
    public async Task<string> CreateProperty(CreateProperty property)
    {
        return "";
    }
}
