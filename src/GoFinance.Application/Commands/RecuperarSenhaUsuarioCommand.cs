using FluentValidation;
using GoFinance.Domain.Core.Messages;

namespace GoFinance.Application.Commands
{
    public class RecuperarSenhaUsuarioCommand : Command
    {
        public RecuperarSenhaUsuarioCommand(string tokenAlterarSenha, string senha)
        {
            TokenAlterarSenha = tokenAlterarSenha;
            Senha = senha;
        }

        public string TokenAlterarSenha { get; private set; }
        public string Senha { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new RecuperarSenhaUsuarioValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class RecuperarSenhaUsuarioValidator : AbstractValidator<RecuperarSenhaUsuarioCommand>
    {
        public RecuperarSenhaUsuarioValidator()
        {
            RuleFor(c => c.TokenAlterarSenha).NotEmpty().NotNull().WithMessage("O Token de alteração da Senha tem que ser preenchido");
            RuleFor(c => c.Senha).MaximumLength(20).WithMessage("O campo Senha não pode ter mais de 20 caracteres");
            RuleFor(c => c.Senha).MinimumLength(6).WithMessage("O campo Senha tem que ter no minímo 6 caracteres");
            RuleFor(c => c.Senha).NotNull().NotEmpty().WithMessage("O campo Senha não pode estar vazio");
        }
    }

}
