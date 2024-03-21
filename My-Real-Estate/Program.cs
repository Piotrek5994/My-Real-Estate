using Core.CommendDto;
using Core.IRepositories;
using Infrastracture.Db;
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
using System.Reflection;
using System.Text;

namespace My_Real_Estate;

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

        builder.Services.Configure<BunnyCdnSettings>(
            builder.Configuration.GetSection("BunnyCDN"));

        builder.Services.AddSingleton<BunnyCdnContext>(sp =>
            new BunnyCdnContext(sp.GetRequiredService<IOptions<BunnyCdnSettings>>()));
        //AutoMapper
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddControllers();
        //Service 
        builder.Services.AddScoped<IAuthService,AuthService>();
        builder.Services.AddScoped<IUserService,UserService>();
        builder.Services.AddScoped<IPhotoService,PhotoService>();
        builder.Services.AddScoped<IAddressService,AddressService>();
        builder.Services.AddScoped<IFeaturesService,FeaturesService>();
        builder.Services.AddScoped<IPaymentService,PaymentService>();
        builder.Services.AddScoped<IPropertyService,PropertyService>();
        builder.Services.AddScoped<IPropertyTypeService,PropertyTypeService>();

        //Repository
        builder.Services.AddScoped<IAuthRepository,AuthRepository>();
        builder.Services.AddScoped<IUserRepository,UserRepository>();
        builder.Services.AddScoped<IPhotoRepository,PhotoRepository>();
        builder.Services.AddScoped<IAddressRepository,AddressRepository>();
        builder.Services.AddScoped<IFeaturesRepository,FeaturesRepository>();
        builder.Services.AddScoped<IPaymentRepository,PaymentRepository>();
        builder.Services.AddScoped<IPropertyRepository,PropertyRepository>();
        builder.Services.AddScoped<IPropertyTypeRepository,PropertyTypeRepository>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.MapType<Stream>(() => new OpenApiSchema { Type = "file", Format = "binary" });

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();

            //Add access for swagger model example
            options.ExampleFilters();

            //Add access for swagger description
            options.EnableAnnotations();

            options.CustomSchemaIds(type =>
            {
                return type.Name switch
                {
                    "CreateLogin" => "Login-Auth",
                    "UpdatePassword" => "Change-Auth",
                    "CreateUserDto" => "Register-User",
                    "UpdateUser" => "Update-User",
                    "CreatePropertyDto" => "Create-Property",
                    "CreatePropertyTypeDto" => "Create-Propert-Type",
                    "UpdateProperty" => "Update-Property",
                    "UpdatePropertyType" => "Update-Property-Type",
                    _ => type.Name
                };
            });
        });
        // Add Swegger Example
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
