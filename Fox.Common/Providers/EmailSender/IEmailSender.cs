using System.Threading.Tasks;

namespace Fox.Common.Providers.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
