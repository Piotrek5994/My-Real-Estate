using Core.Filter;
using Core.IRepositories;
using Infrastructure.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastracture.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly MongoDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _log;

    public PhotoRepository(MongoDbContext context, ILogger<PhotoRepository> log, IUserRepository userRepository)
    {
        _context = context;
        _log = log;
        _userRepository = userRepository;
    }
    public async Task<string> UploudAvatarPhoto(IFormFile formFile, string userId)
    {
        try
        {
            UserFilter userFilter = new UserFilter
            {
                Id = userId,
            };
            var chackUser = await _userRepository.GetUser(userFilter);
            if (chackUser == null)
            {
                return "Chosen user not exist";
            }
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string avatarFolder = Path.Combine(desktopPath, "AvatarPhotos");

            if (!Directory.Exists(avatarFolder))
            {
                Directory.CreateDirectory(avatarFolder);
            }

            string fileName = $"{userId}_avatar{Path.GetExtension(formFile.FileName)}";
            string filePath = Path.Combine(avatarFolder, fileName);

            if (File.Exists(filePath))
            {
                return "This user already has an avatar.";
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return filePath;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error with UploudAvatarPhoto");
            return null;
        }
    }
}
