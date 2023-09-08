namespace Idenitity_Provider.Persistence.Dtos.Auth;

public class LoginDto
{
    public string IdentityProvider { get; set; } = string.Empty;
    public string Password { get; set; } = String.Empty;
}