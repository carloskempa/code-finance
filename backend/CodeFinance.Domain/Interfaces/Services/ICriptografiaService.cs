namespace CodeFinance.Domain.Interfaces.Services
{
    public interface ICriptografiaService
    {
        string Criptografar(string text);
        bool VerificarCriptografia(string text, string stringCriptografado);
    }
}
