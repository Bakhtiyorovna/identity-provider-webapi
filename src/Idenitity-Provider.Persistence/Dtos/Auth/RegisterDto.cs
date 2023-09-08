namespace Idenitity_Provider.Persistence.Dtos.Auth;

public class RegisterDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityProvider { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
