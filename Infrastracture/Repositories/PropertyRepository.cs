using Core.IRepositories;
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
}
