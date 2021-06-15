using GoFinance.Application.Validators;
using GoFinance.Domain.Core.Messages;

namespace GoFinance.Application.Commands
{
    public class AdicionarSmtpCommand: Command
    {
        public AdicionarSmtpCommand(string email, string mascara, string host, int porta, string usuario, string senha, bool sSL, bool ativo)
        {
            Email = email;
            Mascara = mascara;
            Host = host;
            Porta = porta;
            Usuario = usuario;
            Senha = senha;
            SSL = sSL;
            Ativo = ativo;
        }

        public string Email { get; private set; }
        public string Mascara { get; private set; }
        public string Host { get; private set; }
        public int Porta { get; private set; }
        public string Usuario { get; private set; }
        public string Senha { get; private set; }
        public bool SSL { get; private set; }
        public bool Ativo { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarSmtpValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
