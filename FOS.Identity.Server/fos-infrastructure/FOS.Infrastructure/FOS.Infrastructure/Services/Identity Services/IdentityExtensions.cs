
using AMS.API.Authenticator.IdentityServices;
using FOS.Repository.Implementors;
using FOS.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddUserStore(this IIdentityServerBuilder builder,IConfiguration configuration)
        {
            builder.Services.AddSingleton<IUserRepository, UserRepository>(s=>new UserRepository(Convert.ToString(configuration["ConnectionStrings:AMS_CON_STR"])));
            builder.AddProfileService<UserProfileService>();
            builder.AddResourceOwnerValidator<UserResouceOwnerPasswordValidator>();

            return builder;
        }
    }
}