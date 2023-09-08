using Identity_Provider.Service.Interfaces.Auth;
using Identity_Provider.Service.Interfaces.Notifications;
using Identity_Provider.Service.Services.Auth;
using Identity_Provider.Service.Services.Notifications;
using Microsoft.Extensions.Caching.Memory;

namespace Identity_Provider.WebApi.Configurations.Layers
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
        }
    }
}
