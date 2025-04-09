using Clommercy.Core.Shared.Contracts;
using Clommercy.Core.Users.Repository;
using Clommercy.Persistence.AdoNet;
using Clommercy.Persistence.Repository;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clommercy.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer") ?? "";

        services.AddScoped<IAdoNetContext>((_) => new AdoNetContext(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
