using Core.Commend.Create;
using Core.Commend.Update;
using Core.IRepositories;
using Core.Model;
using Infrastracture.Helper;
using Infrastructure.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastracture.Repositories;

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
    public async Task<string> Login(CreateLogin login)
    {
        try
        {
            var collection = _context.GetCollection<User>("User");
            var user = await collection.Find(u => u.Email == login.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                _log.LogWarning("Warning : user not found");
                return "Authentication failed. User not found.";
            }
            else
            {
                if (PasswordHasher.VerifyPassword(login.Password, user.Password))
                {
                    return CreateToken(user);
                }
                else
                {
                    _log.LogWarning("Warning : user password incorrect");
                    return "Authentication failed. Password incorrect.";
                }
            }
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error logging in user : Message {ex.Message}");
            return null;
        }
    }
    public async Task<string> Refresh(string token)
    {
        try
        {
            return RefreshToken(token);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error logging in refresh token : Message {ex.Message}");
            return null;
        }
    }
    public async Task<bool> UpdateRole(string userId, string? role)
    {
        try
        {
            var collection = _context.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(userId));

            bool userExists = await collection.Find(filter).AnyAsync();
            if (!userExists)
            {
                _log.LogWarning($"Warninng : user not exist by id : {userId}.");
                return false;
            }

            var update = new List<UpdateDefinition<User>>();

            if (!string.IsNullOrEmpty(role))
                update.Add(Builders<User>.Update.Set(u => u.Role, role));

            if (update.Count == 0)
            {
                return false;
            }

            var combinedUpdate = Builders<User>.Update.Combine(update);
            var result = await collection.UpdateOneAsync(filter, combinedUpdate);

            return result.ModifiedCount > 0;
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error change user role : Message {ex.Message}");
            return false;
        }
    }
    public async Task<bool> ChangePassword(UpdatePassword password)
    {
        try
        {
            var collection = _context.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(password.userId));

            var user = await collection.Find(filter).FirstOrDefaultAsync();
            if (user == null)
            {
                _log.LogWarning($"Warning : user not exist by id : {password.userId}.");
                return false;
            }
            else
            {
                if (PasswordHasher.VerifyPassword(password.OldPassword, user.Password))
                {
                    password.NewPassword = PasswordHasher.HashPassword(password.NewPassword);
                    var update = Builders<User>.Update.Set(u => u.Password, password.NewPassword);
                    var result = await collection.UpdateOneAsync(filter, update);

                    return result.ModifiedCount > 0;
                }
                else
                {
                    _log.LogWarning($"Warning : change password attempt failed for user ID: {password.userId}. Incorrect password provided.");
                    return false;
                }
            }
        }
        catch (MongoException ex)
        {
            _log.LogError(ex, $"Error change user password : Message {ex.Message}");
            return false;
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
    private string RefreshToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false
        };

        SecurityToken validatedToken;
        ClaimsPrincipal claimsPrincipal = null;
        try
        {
            claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
        }
        catch (Exception ex)
        {
            _log.LogError($"Token validation error: {ex.Message}");
            return null;
        }

        if (validatedToken != null && validatedToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
            var firstName = claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value;
            var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

            var newTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.GivenName, firstName),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var newToken = tokenHandler.CreateToken(newTokenDescriptor);
            _log.LogInformation($"The token has been refreshed for user o ID: {userId}");
            return tokenHandler.WriteToken(newToken);
        }
        _log.LogError("Token refresh failed - invalid token");
        return null;
    }
}
