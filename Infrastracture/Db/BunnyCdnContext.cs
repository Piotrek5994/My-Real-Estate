using BunnyCDN.Net.Storage;
using Infrastracture.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Db
{
    public class BunnyCdnContext
    {
        private readonly BunnyCdnSettings _settings;
        private readonly BunnyCDNStorage _bunnyCdnStorage;

        public BunnyCdnContext(IOptions<BunnyCdnSettings> settings)
        {
            _settings = settings.Value;
            _bunnyCdnStorage = new BunnyCDNStorage(
                _settings.StorageZoneName,
                _settings.ApiKey
            );
        }
        public async Task UploadObjectAsync(Stream stream, string remotePath)
        {
            await _bunnyCdnStorage.UploadAsync(stream, remotePath);
        }
        public async Task<Stream> DownloadObjectAsStreamAsync(string remotePath)
        {
            return await _bunnyCdnStorage.DownloadObjectAsStreamAsync(remotePath);
        }

        public async Task DownloadObjectAsync(string remotePath, string localFilePath)
        {
            await _bunnyCdnStorage.DownloadObjectAsync(remotePath, localFilePath);
        }

        public async Task DeleteObjectAsync(string remotePath)
        {
            await _bunnyCdnStorage.DeleteObjectAsync(remotePath);
        }
    }
}
