namespace Identity_Provider.Application.Exceptions.Users;

public class UserAlreadyExsistExceptions : AlreadyExsistException
{
    public UserAlreadyExsistExceptions()
    {
        TitleMessage = "email already exists";
    }

    public UserAlreadyExsistExceptions(string email)
    {
        TitleMessage = "This email is already registered";
    }
}
