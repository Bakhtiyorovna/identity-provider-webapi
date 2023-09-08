using Idenitity_Provider.Persistence.Dtos.Notifications;

namespace Identity_Provider.Service.Interfaces.Notifications;

public interface IEmailSender
{
    public Task<bool> SenderAsync(EmailMessage emailMessage);
}
