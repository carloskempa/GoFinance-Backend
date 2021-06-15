using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class DeletarSmtpCommand : Command
    {
        public DeletarSmtpCommand(Guid smtpId)
        {
            SmtpId = smtpId;
        }

        public Guid SmtpId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new DeletarContaFinanceiraValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DeletarSmtpValidador : AbstractValidator<DeletarSmtpCommand>
    {
        public DeletarSmtpValidador()
        {
            RuleFor(c => c.SmtpId).NotEqual(Guid.Empty)
                                              .WithMessage("Id Smtp é inválido");
        }
    }

}
