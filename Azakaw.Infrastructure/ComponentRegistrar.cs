using Azakaw.Data.Context;
using Azakaw.Data.Repositories;
using Azakaw.Domain.Models;
using Azakaw.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Azakaw.Infrastructure
{
    public static class ComponentRegistrar
    {
        // Register all your components here
        public static void RegisterComponents(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(builder => builder.UseInMemoryDatabase("AzakawDB"));
            services.AddRepositories();
            services.AddServices();
        }

        // Add repositories here
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Complaint>, BaseRepository<Complaint>>();
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        }

        // Add services here
        private static void AddServices(this IServiceCollection services)
        {
        }
    }
}