using DAO.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using DAO.Services;

namespace DAO.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static void Migrate(this IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<BlogContext>().Database.Migrate();
            }
        }

        public static void Seed(this IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var passwordService = serviceScope.ServiceProvider.GetService<IHashPasswordService>();
                serviceScope.ServiceProvider.GetService<BlogContext>().EnsureSeeded(passwordService);
            }
        }
    }
}
