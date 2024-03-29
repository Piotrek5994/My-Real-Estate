using BunnyCDN.Net.Storage;
using Core.Commend.Create;
using Core.Filter;
using Core.IRepositories;
using Core.Model;
using Infrastracture.Db;
using Infrastructure.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Writers.Zip;

namespace Infrastracture.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly MongoDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly ILogger _log;
    private readonly BunnyCdnContext _bunnyContext;

    public PhotoRepository(MongoDbContext context, ILogger<PhotoRepository> log, IUserRepository userRepository, BunnyCdnContext bunnyContext, IPropertyRepository propertyRepository)
    {
        _context = context;
        _log = log;
        _userRepository = userRepository;
        _bunnyContext = bunnyContext;
        _propertyRepository = propertyRepository;
    }
    public async Task<Stream> GetAvatarPhoto(string userId)
    {
        try
        {
            var chackUser = await _userRepository.GetUser(new UserFilter { Id = userId });
            if (chackUser == null || !chackUser.Any())
            {
                _log.LogWarning("Warning: chosen user does not exist");
                return null;
            }

            var collection = _context.GetCollection<Avatar>("Avatar");
            var userFilter = await collection.Find(u => u.UserId == userId).FirstOrDefaultAsync();
            if (userFilter == null)
            {
                _log.LogWarning("Warning: user does not have an avatar");
            }

            var stream = await _bunnyContext.DownloadObjectAsStreamAsync(userFilter.AvatarScr);
            return stream;

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
    }
    public async Task<string> UploudAvatarPhoto(IFormFile formFile, string userId)
    {
        try
        {
            var chackUser = await _userRepository.GetUser(new UserFilter { Id = userId });
            if (chackUser == null || !chackUser.Any())
            {
                _log.LogWarning("Warning: chosen user does not exist");
                return "Chosen user does not exist";
            }

            string fileName = $"/myrealestate/{userId}_avatar{Path.GetExtension(formFile.FileName)}";

            var collection = _context.GetCollection<CreateAvatar>("Avatar");
            var userAvatar = await collection.Find(u => u.AvatarScr == fileName).FirstOrDefaultAsync();
            if (userAvatar != null)
            {
                _log.LogWarning("Warning: user already has an avatar");
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
            var chackUser = await _userRepository.GetUser(new UserFilter { Id = userId });
            if (chackUser == null || !chackUser.Any())
            {
                _log.LogWarning("Warning: chosen user does not exist");
                return "Chosen user does not exist";
            }

            await DeleteAvatarPhoto(userId);

            var collection = _context.GetCollection<Avatar>("Avatar");
            var userAvatar = await collection.Find(u => u.UserId == userId).FirstOrDefaultAsync();

            string fileName = $"/myrealestate/{userId}_avatar{Path.GetExtension(formFile.FileName)}";

            using (var stream = formFile.OpenReadStream())
            {
                await _bunnyContext.UploadObjectAsync(stream, fileName);
            };

            if (userAvatar.AvatarScr == null)
            {
                var filter = Builders<Avatar>.Filter.Eq(a => a.UserId, userId);
                var update = Builders<Avatar>.Update.Set(a => a.AvatarScr, fileName);
                await collection.UpdateOneAsync(filter, update);

                return "Avatar changed successfully";
            }
            else
            {
                await UploudAvatarPhoto(formFile, userId);

                return "Avatar create successfully";
            }

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
    }
    public async Task DeleteAvatarPhoto(string userId)
    {
        try
        {
            var chackUser = await _userRepository.GetUser(new UserFilter { Id = userId });
            if (chackUser == null || !chackUser.Any())
            {
                _log.LogWarning("Warning: chosen user does not exist");
            }

            var collection = _context.GetCollection<Avatar>("Avatar");

            var userFilter = await collection.Find(u => u.UserId == userId).FirstOrDefaultAsync();
            if (userFilter != null && userFilter.AvatarScr != null)
            {
                await _bunnyContext.DeleteObjectAsync(userFilter.AvatarScr);
            }

            var filter = Builders<Avatar>.Filter.Eq(a => a.UserId, userId);
            var update = Builders<Avatar>.Update.Set(a => a.AvatarScr, null);

            await collection.UpdateOneAsync(filter, update);
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
    public async Task<List<Stream>> GetPropertyPhoto(string propertyId)
    {
        try
        {
            return null;
        }
        catch (BunnyCDNStorageException ex)
        {
            return null;
        }
        catch (MongoException ex)
        {
            return null;
        }
    }
    public async Task<List<string>> UploudsPropertyPhotos(IFormFileCollection formFilesCollection, string propertyId)
    {
        try
        {
            List<CreatePhoto> photos = new List<CreatePhoto>();
            List<string> message = new List<string>();
            List<string> photoIds = new List<string>();

            var checkProperty = await _propertyRepository.GetProperty(new PropertyFilter { Id = propertyId });
            if (checkProperty == null || !checkProperty.Any())
            {
                _log.LogWarning("Warning: chosen property does not exist");
                message.Add("Chosen property does not exist");
                return message;
            }

            var collection = _context.GetCollection<CreatePhoto>("Photo");

            foreach (var file in formFilesCollection)
            {
                if (file.Length > 0)
                {
                    var photo = new CreatePhoto();
                    photo.Id = ObjectId.GenerateNewId().ToString();
                    string fileName = $"/myrealestate/{photo.Id}_photo{file.FileName}";
                    photo.PhotoSrc = fileName;
                    photo.PropertyId = propertyId;

                    photos.Add(photo);
                    photoIds.Add(photo.Id);

                    using (var stream = file.OpenReadStream())
                    {
                        await _bunnyContext.UploadObjectAsync(stream, fileName);
                    }
                }
            }

            if (photos.Any())
            {
                await collection.InsertManyAsync(photos);
            }

            return photoIds;
        }
        catch (BunnyCDNStorageException ex)
        {
            _log.LogError(ex, $"Error with BunnyCDN Storage: {ex.Message}");
            return new List<string> { $"Error with BunnyCDN Storage: {ex.Message}" };
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error with CreatePhotoforProperty in MongoDb :: {ex.Message}");
            return new List<string> { $"Error with CreatePhotoforProperty in MongoDb :: {ex.Message}" };
        }
    }
    public async Task<List<string>> ChangesPropertyPhotos(IFormFileCollection formFilesCollection, string photoId, string propertyId)
    {
        try
        {

            List<string> uploadedFiles = new List<string>();

            foreach (var file in formFilesCollection)
            {
                if (file.Length > 0)
                {
                    string pathInStorageZone = $"/{propertyId}/{photoId}_{file.FileName}";

                    using (var stream = file.OpenReadStream())
                    {
                        await _bunnyContext.UploadObjectAsync(stream, pathInStorageZone);

                        uploadedFiles.Add(pathInStorageZone);
                    }
                }
            }

            return uploadedFiles;
        }
        catch (BunnyCDNStorageException ex)
        {
            return null;
        }
        catch (MongoException ex)
        {
            return null;
        }
    }
    public async Task<bool> DeletePropertyPhotos(List<string> photoId)
    {
        try
        {
            return true;
        }
        catch (BunnyCDNStorageException ex)
        {
            return false;
        }
        catch (MongoException ex)
        {
            return false;
        }
    }
}
