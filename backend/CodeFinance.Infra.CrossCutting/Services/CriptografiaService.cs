using CodeFinance.Domain.Interfaces.Services;

namespace CodeFinance.Infra.CrossCutting.Services
{
    public class CriptografiaService : ICriptografiaService
    {
        public string Criptografar(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }

        public bool VerificarCriptografia(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
    }
}