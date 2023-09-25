using Identity_Provider.DataAccess.Interfaces.Users;
using Identity_Provider.DataAccess.Repositories.Users;

namespace Identity_Provider.WebApi.Configurations.Layers
{
    public static class DataAccessConfigurations
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepositoy>();
        }
    }
}
