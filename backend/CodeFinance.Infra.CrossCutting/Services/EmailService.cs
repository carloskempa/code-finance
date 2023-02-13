using CodeFinance.Domain.Interfaces.Services;
using CodeFinance.Infra.CrossCutting.Models;
using System.Threading.Tasks;

namespace CodeFinance.Infra.CrossCutting.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpConfig _smtpConfig;
        public EmailService(SmtpConfig smtpConfig)
        {
            _smtpConfig = smtpConfig;
        }

        public Task Enviar(string email, string title, string body)
        {
            return Task.CompletedTask;
        }

        public static class EmailServiceFactory
        {
           public static EmailService Create(SmtpConfig smtpConfig) => new(smtpConfig);
        }
    }
}
