using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class DepositarSaldoCommand : Command
    {
        public DepositarSaldoCommand(Guid contaFinanceiraId, Guid usuarioId, decimal valor)
        {
            ContaFinanceiraId = contaFinanceiraId;
            UsuarioId = usuarioId;
            Valor = valor;
        }

        public Guid ContaFinanceiraId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public decimal Valor { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new DepositarSaldoValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DepositarSaldoValidator : AbstractValidator<DepositarSaldoCommand>
    {
        public DepositarSaldoValidator()
        {
            RuleFor(c => c.ContaFinanceiraId).NotEqual(Guid.Empty)
                                 .WithMessage("Id da conta financeira inválido");

            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty)
                                 .WithMessage("Id do usuário é inválido");

            RuleFor(c => c.Valor).NotEmpty().WithMessage("Valor deve ser informado");
        }
    }
}
