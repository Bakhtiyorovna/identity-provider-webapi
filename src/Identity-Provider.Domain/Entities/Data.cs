
using Identity_Provider.Domain.Entities.Acconts;

namespace Identity_Provider.Domain.Entities;

public class Data
{
    public static List<AccountModel> NameList = new List<AccountModel>
    {
        new AccountModel{FirstName="A", LastName="a"},
        new AccountModel{FirstName="B", LastName="b"},
    };
}
