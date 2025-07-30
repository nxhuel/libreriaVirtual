using libreriaVirtual.Context;
using libreriaVirtual.Models;
using Microsoft.AspNetCore.Identity;

namespace libreriaVirtual.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<UserModel, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });

            return services;
        }
    }
}
