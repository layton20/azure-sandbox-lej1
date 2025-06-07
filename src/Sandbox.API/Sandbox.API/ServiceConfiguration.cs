using Microsoft.EntityFrameworkCore;
using Sandbox.API.Data;
using Sandbox.API.Managers;
using Sandbox.API.Repositories;
using Sandbox.API.Settings;

namespace Sandbox.API;

internal static class ServiceConfiguration
{
    internal static void RegisterDependencies(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .Configure<GlobalSettings>(configuration.GetSection(GlobalSettings.AppSettingsSection))
            .RegisterDatabases(configuration)
            .RegisterRepositories()
            .RegisterManagers();
    }

    internal static IServiceCollection RegisterRepositories(this IServiceCollection service)
    {
        return service.AddScoped<ICustomerRepository, CustomerRepository>();
    }

    internal static IServiceCollection RegisterManagers(this IServiceCollection service)
    {
        return service.AddScoped<ICustomerManager, CustomerManager>();
    }

    internal static IServiceCollection RegisterDatabases(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        GlobalSettings? _Settings = configuration.GetSection(GlobalSettings.AppSettingsSection).Get<GlobalSettings>();

        if (string.IsNullOrWhiteSpace(_Settings?.Database.SandboxURL))
            throw new InvalidOperationException("Database connection string not found in configuration.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_Settings.Database.SandboxURL));

        return services;
    }
}