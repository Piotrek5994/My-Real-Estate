using BunnyCDN.Net.Storage;
using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Infrastracture.Db;
using Infrastructure.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastracture.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly MongoDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _log;
    private readonly BunnyCdnContext _bunnyContext;

    public PhotoRepository(MongoDbContext context, ILogger<PhotoRepository> log, IUserRepository userRepository, BunnyCdnContext bunnyContext)
    {
        _context = context;
        _log = log;
        _userRepository = userRepository;
        _bunnyContext = bunnyContext;
    }
    public async Task<string> GetAvatarPhoto(string userId)
    {
        try
        {

        }
        catch (BunnyCDNStorageException ex)
        {
            _log.LogError(ex, $"Error with BunnyCDN Storage: {ex.Message}");
            return null;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error with GetAvatarPhoto in MongoDb : {ex.Message}");
            return null;
        }
        return "";
    }
    public async Task<string> UploudAvatarPhoto(IFormFile formFile, string userId)
    {
        try
        {
            var chackUser = await _userRepository.GetUser(new UserFilter { Id = userId });
            if (chackUser == null)
            {
                _log.LogError("Error: chosen user does not exist");
                return "Chosen user does not exist";
            }

            string fileName = $"/myrealestate/{userId}_avatar{Path.GetExtension(formFile.FileName)}";

            var collection = _context.GetCollection<CreateAvatar>("Avatar");
            var userAvatar = await collection.Find(u => u.AvatarScr == fileName).FirstOrDefaultAsync();
            if (userAvatar != null)
            {
                _log.LogError("Error: user already has an avatar");
                return "The user already has an avatar";
            }

            using (var stream = formFile.OpenReadStream())
            {
                await _bunnyContext.UploadObjectAsync(stream, fileName);
            }

            CreateAvatar create = new CreateAvatar
            {
                AvatarScr = fileName,
                UserId = userId,
            };

            await collection.InsertOneAsync(create);

            return create.Id;
        }
        catch (BunnyCDNStorageException ex)
        {
            _log.LogError(ex, $"Error with BunnyCDN Storage: {ex.Message}");
            return null;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error with UploudAvatarPhoto in MongoDb : {ex.Message}");
            return null;
        }
    }
    public async Task<string> ChangeAvatarPhoto(IFormFile formFile, string userId)
    {
        try
        {

        }
        catch (BunnyCDNStorageException ex)
        {
            _log.LogError(ex, $"Error with BunnyCDN Storage: {ex.Message}");
            return null;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error with ChangeAvatarPhoto in MongoDb : {ex.Message}");
            return null;
        }
        return "";
    }
    public async Task DeleteAvatarPhoto(string userId)
    {
        try
        {

        }
        catch (BunnyCDNStorageException ex)
        {
            _log.LogError(ex, $"Error with BunnyCDN Storage: {ex.Message}");
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error with DeleteAvatarPhoto in MongoDb : {ex.Message}");
        }
    }
}
