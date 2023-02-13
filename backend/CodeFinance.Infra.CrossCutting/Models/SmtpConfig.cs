namespace CodeFinance.Infra.CrossCutting.Models
{
    public class SmtpConfig
    {
        public string Email { get; set; }
        public string Mascara { get; set; }
        public string Host { get; set; }
        public int Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool Ssl { get; set; }
    }
}
