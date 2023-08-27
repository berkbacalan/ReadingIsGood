using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Domain.Repositories.Base;
using ReadingIsGood.Infrastructure.Data;
using ReadingIsGood.Infrastructure.Repositories;
using ReadingIsGood.Infrastructure.Repositories.Base;

namespace ReadingIsGood.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(databaseName:
                "InMemoryDb"),
            ServiceLifetime.Singleton,
            ServiceLifetime.Singleton);

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IBookRepository, BookRepository>();

        return services;
    }
}