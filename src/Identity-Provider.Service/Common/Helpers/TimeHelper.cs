using Identity_Provider.Domain.Constants;

namespace Identity_Provider.Service.Common.Helpers;

public class TimeHelper
{
    public static DateTime GetDateTime()
    {
        var time = DateTime.UtcNow;

        time = time.AddHours(TimeConstant.UTC);

        return time;
    }
}
