using System.Text;
using JHT.Logistics.Application.Interfaces;
using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Application.Interfaces.Services;
using JHT.Logistics.Infrastructure.Data;
using JHT.Logistics.Infrastructure.Data.Interceptors;
using JHT.Logistics.Infrastructure.Data.Repositories;
using JHT.Logistics.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JHT.Logistics.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Interceptor
        services.AddScoped<JHT.Logistics.Infrastructure.Data.Interceptors.AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<JHT.Logistics.Infrastructure.Data.Interceptors.AuditoriaSaveChangesInterceptor>();

        // DbContext
        services.AddDbContext<JhtDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(
                sp.GetRequiredService<JHT.Logistics.Infrastructure.Data.Interceptors.AuditableEntitySaveChangesInterceptor>(),
                sp.GetRequiredService<JHT.Logistics.Infrastructure.Data.Interceptors.AuditoriaSaveChangesInterceptor>()
            );
        });

        // Services
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // Repositories
        services.AddScoped<JHT.Logistics.Application.Interfaces.Repositories.IClienteRepository, JHT.Logistics.Infrastructure.Data.Repositories.ClienteRepository>();
        services.AddScoped<JHT.Logistics.Application.Interfaces.Repositories.IConductorRepository, JHT.Logistics.Infrastructure.Data.Repositories.ConductorRepository>();
        services.AddScoped<JHT.Logistics.Application.Interfaces.Repositories.IVehiculoRepository, JHT.Logistics.Infrastructure.Data.Repositories.VehiculoRepository>();
        services.AddScoped<JHT.Logistics.Application.Interfaces.Repositories.IServicioRepository, JHT.Logistics.Infrastructure.Data.Repositories.ServicioRepository>();
        services.AddScoped<JHT.Logistics.Application.Interfaces.Services.IStorageService, JHT.Logistics.Infrastructure.Services.MinioStorageService>();

        // Autenticación JWT
        services.AddAuthentication(options =>
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
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "default_super_secret_key_change_me!_1234567890"))
            };
        });

        services.AddAuthorization();

        // Register Services
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}
