namespace Identity_Provider.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string IdentityProvider { get; set; } = String.Empty;
    public string ProviderKey { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public string Salt { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public bool Confirm { get; set; }
}
