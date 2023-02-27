using SmartTestTask.Application.Interfaces.Services;

namespace SmartTestTask.API.Extensions;

public static class HostExtensions
{
    public static async Task SeedDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var databaseSeeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
        await databaseSeeder.SeedDatabaseAsync();
    }
}