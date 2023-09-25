using Identity_Provider.Domain.Entities.Users;

namespace Identity_Provider.Service.Interfaces.Auth;

public interface ITokenService
{
    public string GenereateToken(User user);
}
