using FluentValidation;
using FluentValidation.AspNetCore;
using LibrariesWeb.Application.Constants;
using LibrariesWeb.Application.Helpers;
using LibrariesWeb.Application.MappingProfiles;
using LibrariesWeb.Application.Services.Implementations;
using LibrariesWeb.Application.Services.Interfaces;
using LibrariesWeb.Application.Validator;
using LibrariesWeb.Domain.Repositories.Interfaces;
using LibrariesWeb.Persistance.Extensions;
using LibrariesWeb.Persistance.Repositories.Implementation;
using LibrariesWeb.Persistance.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LibrariesWeb.API.Extensions;

public static partial class ApplicationDependenciesConfiguration
{
    
    public static IServiceCollection ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddLogger();
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole(Roles.Admin));
            options.AddPolicy("UserOnly", policy => policy.RequireRole(Roles.User));
            options.AddPolicy("AdminOrUser", policy => policy.RequireRole(Roles.Admin, Roles.User));
        });
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
        builder.Services.AddIdentityDatabase(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        })
           .AddSwaggerGen(c =>
           {
               c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   In = ParameterLocation.Header,
                   Description = "Please Enter token",
                   Name = "Authorization",
                   Type = SecuritySchemeType.Http,
                   BearerFormat = "Jwt",
                   Scheme = "bearer"
               });
               c.AddSecurityRequirement(new OpenApiSecurityRequirement
               {
                  {

                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           }
                       },
                      new List<string>()
                  }
               });
           })

           .AddCors(options =>
           {
               options.AddPolicy("MyPolicy", opt =>
               {
                   opt.AllowAnyHeader()
                   .AllowAnyOrigin()
                   .AllowAnyMethod();
               });
           })
           .AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>()
           .AddFluentValidationAutoValidation()
           .AddAutoMapper(typeof(ApplicationMapping))
           .AddRepositories()
           .AddServices();

        return builder.Services;
    }
    public async static Task Configure(this WebApplication application)
    {
        application.UseCors("MyPolicy");

        await application.UseMigration();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IBookServices, BookServices>()
            .AddScoped<IAuthorServices, AuthorServices>()
            //.AddScoped<IIssuedBookService, IssuedBookService>()
            .AddScoped<SeedRole>()
            .AddScoped<SeedAdmin>();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IAuthorRepository, AuthorRepository>();
            
    }
}