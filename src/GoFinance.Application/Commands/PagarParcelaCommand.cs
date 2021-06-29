using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class PagarParcelaCommand : Command
    {
        public Guid ParcelaId { get; set; }
        public decimal Valor { get; set; }
        public Guid ContaFinanceiraId { get; set; }
        public decimal Multa { get; set; }
        public decimal Juros { get; set; }
        public decimal Desconto { get; set; }

        public override bool EhValido()
        {
            return base.EhValido();
        }
    }
    public class PagarParcelaValidator : AbstractValidator<PagarParcelaCommand>
    {
        public PagarParcelaValidator()
        {
            RuleFor(c => c.ParcelaId).NotEqual(Guid.Empty).WithMessage("Id da Parcela inválido");
            RuleFor(c => c.ContaFinanceiraId).NotEqual(Guid.Empty).WithMessage("Id Conta Financeira inválido");
            RuleFor(c => c.Valor).GreaterThan(0).WithMessage("O valor do pagamento tem que ser mair que zero");
        }
    }
}
