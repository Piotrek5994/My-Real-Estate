using Core.Command.Update;
using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories;

public class FeaturesRepository : IFeaturesRepository
{
    private readonly MongoDbContext _context;
    private readonly ILogger _log;

    public FeaturesRepository(MongoDbContext context, ILogger<FeaturesRepository> log)
    {
        _context = context;
        _log = log;
    }
    public async Task<List<Features>> GetFeatures(FeaturesFilter filter)
    {
        try
        {
            return null;  
        }
        catch(MongoException ex)
        {
            _log.LogError(ex, $"Error getting feature(s) type : Message - {ex.Message}");
            return null;
        }
    }
    public async Task<List<string>> CreateFeatures(CreateFeatures features,string propertyId)
    {
        try
        {
            return null;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create feature(s) type : Message - {ex.Message}");
            return null;
        }
    }
    public async Task<bool> UpdateFeature(UpdateFeature feature,string propertyId)
    {
        try
        {
            return true;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create feature(s) type : Message - {ex.Message}");
            return false;
        }
    }
}
