using Core.IRepositories;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories;

public class PropertyTypeRepository : IPropertyTypeRepository
{
    private readonly MongoDbContext _context;
    private readonly ILogger _log;

    public PropertyTypeRepository(MongoDbContext context, ILogger<PropertyTypeRepository> log)
    {
        _context = context;
        _log = log;
    }
}
