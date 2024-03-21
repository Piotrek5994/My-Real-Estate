﻿using Core.Command.Update;
using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastracture.Repositories;

public class FeaturesRepository : IFeaturesRepository
{
    private readonly MongoDbContext _context;
    private readonly IPropertyRepository _propertyRepository;
    private readonly ILogger _log;

    public FeaturesRepository(MongoDbContext context, ILogger<FeaturesRepository> log, IPropertyRepository propertyRepository)
    {
        _context = context;
        _log = log;
        _propertyRepository = propertyRepository;
    }
    public async Task<List<Features>> GetFeatures(FeaturesFilter filter)
    {
        try
        {
            return null;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error getting feature(s) type : Message - {ex.Message}");
            return null;
        }
    }
    public async Task<List<string>> CreateFeatures(List<CreateFeatures> features, string propertyId)
    {
        var resultMessages = new List<string>();
        try
        {

            var checkProperty = await _propertyRepository.GetProperty(new PropertyFilter { Id = propertyId });
            if (checkProperty == null || !checkProperty.Any())
            {
                _log.LogWarning("Warning: chosen property does not exist");
                resultMessages.Add("The chosen property not exist");
                return resultMessages;
            }

            foreach (var feature in features)
            {
                feature.Id = ObjectId.GenerateNewId().ToString();
                feature.PropertyId = propertyId;
            }

            var collection = _context.GetCollection<CreateFeatures>("Features");

            await collection.InsertManyAsync(features);

            var createdFeatures = features.Select(feature => feature.Id).ToList();
            return createdFeatures;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error create feature(s) type : Message - {ex.Message}");
            resultMessages.Add($"An error occurred while creating features: {ex.Message}");
            return resultMessages;
        }
    }
    public async Task<bool> UpdateFeature(UpdateFeature feature, string propertyId)
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
