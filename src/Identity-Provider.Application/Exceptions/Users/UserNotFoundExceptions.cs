namespace Identity_Provider.Application.Exceptions.Users;

public class UserNotFoundExceptions : NotFoundException
{
    public UserNotFoundExceptions()
    {
        this.TitleMessage = "User not found";
    }
}
