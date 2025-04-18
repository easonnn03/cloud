using cloudass.Models;

namespace cloudass.Services
{
    public interface IEmailQueue
    {
        Task SendEmailMessageAsync(EmailRequiredModel e, CancellationToken ct = default);
    }
}

