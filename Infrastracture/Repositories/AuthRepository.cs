using Core.Commend;
using Core.IRepositories;
using Core.Model;
using Infrastructure.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastracture.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MongoDbContext _context;
        private readonly ILogger _log;
        private readonly string _jwtSecret;

        public AuthRepository(MongoDbContext context, ILogger<AuthRepository> log, IConfiguration config)
        {
            _context = context;
            _log = log;
            _jwtSecret = config.GetSection("AppSettings:Token").Value!;
        }
        public async Task<string> CreateUser(CreateUser user)
        {
            try
            {
                user.Role = "User";
                var collection = _context.GetCollection<CreateUser>("User");
                await collection.InsertOneAsync(user);
                return user.Id;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error creating user in MongoDB");
                return "faile";
            }
        }
        public async Task<string> CreateAdmin(CreateUser user)
        {
            try
            {
                user.Role = "Admin";
                var collection = _context.GetCollection<CreateUser>("User");
                await collection.InsertOneAsync(user);
                return user.Id;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error creating user in MongoDB");
                return "faile";
            }
        }
        public async Task<string> Login(CreateLogin login)
        {
            try
            {
                var collection = _context.GetCollection<User>("User");
                var user = await collection.Find(u => u.Email == login.Email && u.Password == login.Password).FirstOrDefaultAsync();
                if (user == null)
                {
                    return "Authentication failed. User not found or password incorrect.";
                }

                return CreateToken(user);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error logging in user");
                return null;
            }
        }

        private string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
