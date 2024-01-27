using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CookbookDbContext>(options =>
                options.UseSqlServer(configuration.
                GetConnectionString("CookbookConnectionString")));

            return services;
        }
    }
}
