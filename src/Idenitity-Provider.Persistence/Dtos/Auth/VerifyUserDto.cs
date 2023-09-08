namespace Idenitity_Provider.Persistence.Dtos.Auth
{
    public class VerifyUserDto
    {
        public string IdentityProvider { get; set; } = String.Empty;
        public int code { get; set; }
    }
}
