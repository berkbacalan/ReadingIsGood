using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingIsGood.Infrastructure.Data;

namespace ReadingIsGood.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(databaseName:
                "InMemoryDb"),
            ServiceLifetime.Singleton,
            ServiceLifetime.Singleton);
        return services;
    }
}