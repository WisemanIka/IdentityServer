using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Fox.Common.Providers.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _enableSsl;
        private readonly string _userName;
        private readonly string _password;

        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            this._host = host;
            this._port = port;
            this._enableSsl = enableSSL;
            this._userName = userName;
            this._password = password;
        }

        // Configuration to Send the Email by Using SmtpClient
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSsl
            };

            return client.SendMailAsync(
                new MailMessage(_userName, email, subject, htmlMessage) { IsBodyHtml = true }
            );
        }
    }
}
