using FlashOrder.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FlashOrder.Configurations
{
    public static class IdentityExtensions
    {
            public static void ConfigureIdentity(this IServiceCollection services)
            {
                var builder = services.AddIdentityCore<ApiUser>(q=>q.User.RequireUniqueEmail=true);

                builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),services);
                builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
            }
    }
}