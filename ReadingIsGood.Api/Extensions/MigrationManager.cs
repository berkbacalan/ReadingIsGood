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
                var orderContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
                
            }
            catch (Exception e)
            {
                throw;
            }
        }
        return host;
    }
}