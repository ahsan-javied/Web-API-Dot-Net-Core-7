using DAL;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Services.Helpers;
using Services.User;

namespace WebAPI.Helpers
{
    public static class ServiceDependency
    {
        public static void AddServicesDependencies(this IServiceCollection services)
        {
            //AddSingleton
            services.AddSingleton<IJWTManager, JWTManager>();

            //AddScoped

            //Unit of Work or repository is always transient as it does not then make cache issues.
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();

        }
    }
}
