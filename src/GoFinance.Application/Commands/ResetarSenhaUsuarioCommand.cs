using FluentValidation;
using GoFinance.Domain.Core.Messages;

namespace GoFinance.Application.Commands
{
    public class ResetarSenhaUsuarioCommand : Command
    {
        public ResetarSenhaUsuarioCommand(string email, string urlSite)
        {
            Email = email;
            UrlSite = urlSite;
        }

        public string Email { get; private set; }
        public string UrlSite { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new ResetarSenhaUsuarioValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ResetarSenhaUsuarioValidator : AbstractValidator<ResetarSenhaUsuarioCommand>
    {
        public ResetarSenhaUsuarioValidator()
        {
            RuleFor(c => c.Email).NotNull().NotEmpty().WithMessage("O campo e-mail deve ser preenchido.");
            RuleFor(c => c.Email).EmailAddress().WithMessage("E-mail inválido");
            RuleFor(c => c.UrlSite).NotNull().NotEmpty().WithMessage("O campo endereço email deve ser preenchido.");
        }
    }

}
