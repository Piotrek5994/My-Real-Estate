﻿using Core.IRepositories;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;

namespace Infrastracture.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly MongoDbContext _context;
    private readonly ILogger _log;

    public PhotoRepository(MongoDbContext context, ILogger<PhotoRepository> log)
    {
        _context = context;
        _log = log;
    }
}
