using GoFinance.Application.Validators;
using GoFinance.Domain.Core.Messages;
using GoFinance.Domain.Enuns;

namespace GoFinance.Application.Commands
{
    public class AdicionarUsuarioCommand : Command
    {
        public AdicionarUsuarioCommand(string nome, string login, string senha, string email, Perfil perfil)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Email = email;
            Perfil = perfil;
        }

        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Email { get; private set; }
        public Perfil Perfil { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarUsuarioValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
