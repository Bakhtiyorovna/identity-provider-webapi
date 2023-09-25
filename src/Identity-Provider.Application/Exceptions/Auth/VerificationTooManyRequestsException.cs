
namespace Identity_Provider.Application.Exceptions.Auth
{
    public class VerificationTooManyRequestsException : TooManyRequestsException
    {
        public VerificationTooManyRequestsException()
        {
            TitleMessage = "You tried more than limits!";
        }
    }
}
