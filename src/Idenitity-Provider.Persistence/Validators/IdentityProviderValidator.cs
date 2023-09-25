namespace Idenitity_Provider.Persistence.Validators;

public class IdentityProviderValidator
{
    public static bool IsValid(string Email)
    {
        int temp = 0;
        for (int i = 0; i < Email.Length; i++)
        {
            if (Email[i] == '@')
            {
                temp += 1;
            }
        }
        if (temp == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
