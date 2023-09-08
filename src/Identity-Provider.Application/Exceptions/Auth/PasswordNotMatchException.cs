
namespace Identity_Provider.Application.Exceptions.Auth;

public class PasswordNotMatchException : BadRequestException
{
    public PasswordNotMatchException()
    {
        this.TitleMessage = "Password is invalid!";
    }
}
