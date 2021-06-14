using System;
using FluentValidation;
using GoFinance.Domain.Core.Messages;

namespace GoFinance.Application.Commands
{
    public class DeletarContaFinanceiraCommand : Command
    {
        public DeletarContaFinanceiraCommand(Guid contaFinanceiraId, Guid usuarioId)
        {
            ContaFinanceiraId = contaFinanceiraId;
            UsuarioId = usuarioId;
        }

        public Guid ContaFinanceiraId { get; private set; }
        public Guid UsuarioId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new DeletarContaFinanceiraValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class DeletarContaFinanceiraValidator : AbstractValidator<DeletarContaFinanceiraCommand>
    {
        public DeletarContaFinanceiraValidator()
        {
            RuleFor(c => c.ContaFinanceiraId).NotEqual(Guid.Empty).WithMessage("Id da conta financeira é inválido");
            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty).WithMessage("Id do usuário inválido");
        }
    }
}
