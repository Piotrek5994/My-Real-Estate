using Core.CommendDto;
using Core.IRepositories;
using Infrastracture.Repositories;
using Infrastracture.Service;
using Infrastracture.Service.IService;
using Infrastracture.Settings;
using Infrastructure.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace My_Real_Estate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure logging
            builder.Logging.ClearProviders(); 
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Information);

            // Add services to the container.
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDb"));

            builder.Services.AddSingleton<MongoDbContext>(sp =>
                new MongoDbContext(sp.GetRequiredService<IOptions<MongoDbSettings>>()));

            //AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllers();
            //Service 
            builder.Services.AddScoped<IAuthService,AuthService>();
            //Repository
            builder.Services.AddScoped<IAuthRepository,AuthRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.ExampleFilters();
            });

            builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateUserDto>();

            // Configure JWT authentication for the application.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration.GetSection("AppSettings:Token").Value!))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Headers["Authorization"].FirstOrDefault();

                        if(!string.IsNullOrEmpty(token))
                        {
                            if(token.StartsWith("Bearer "))
                            {
                                token = token.Substring("Bearer ".Length);
                            }
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
