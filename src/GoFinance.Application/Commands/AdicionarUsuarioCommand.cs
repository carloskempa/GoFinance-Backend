using GoFinance.Application.Validators;
using GoFinance.Domain.Core.Messages;

namespace GoFinance.Application.Commands
{
    public class AdicionarUsuarioCommand : Command
    {
        public AdicionarUsuarioCommand(string nome, string login, string senha, string email, bool administrador)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Email = email;
            Administrador = administrador;
        }

        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Email { get; private set; }
        public bool Administrador { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarUsuarioValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
