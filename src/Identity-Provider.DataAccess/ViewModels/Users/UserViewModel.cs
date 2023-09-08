namespace Identity_Provider.DataAccess.ViewModels.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityProvider { get; set; } = string.Empty;
    public bool Confirm { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
