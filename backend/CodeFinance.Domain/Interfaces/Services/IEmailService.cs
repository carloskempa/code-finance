using System.Threading.Tasks;

namespace CodeFinance.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task Enviar(string email, string title, string body);
    }
}
