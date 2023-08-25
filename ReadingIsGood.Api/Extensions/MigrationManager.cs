using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Infrastructure.Data;

namespace ReadingIsGood.Api.Extensions;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            try
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (appDbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    appDbContext.Database.Migrate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during the Migration, Error: {e}");
            }
        }
        return host;
    }
}