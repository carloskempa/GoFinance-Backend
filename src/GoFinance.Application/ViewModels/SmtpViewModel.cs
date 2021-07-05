namespace GoFinance.Application.ViewModels
{
    public class SmtpViewModel
    {
        public string Email { get; set; }
        public string Mascara { get; set; }
        public string Host { get; set; }
        public int Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool SSL { get; set; }
        public bool Ativo { get; set; }
    }
}
