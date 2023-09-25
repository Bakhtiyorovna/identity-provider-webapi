
namespace Identity_Provider.Application.Exceptions.Users;

public class UserExpiredException : ExpiredException
{
    public UserExpiredException()
    {
        TitleMessage = "Customer data has expired!";
    }
}
