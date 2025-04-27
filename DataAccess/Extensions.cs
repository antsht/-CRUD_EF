using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        // var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        var connectionString = "Server=localhost;Database=Notes;Trusted_Connection=True;TrustServerCertificate=True;";
        serviceCollection.AddDbContext<AppContext>(x => x.UseSqlServer(connectionString));
        serviceCollection.AddScoped<INoteRepository, NoteRepository>();
        return serviceCollection;
    }
}
