using FluentValidation;
using GoFinance.Domain.Core.Messages;

namespace GoFinance.Application.Commands
{
    public class AtualizarContaFinanceiraCommand : Command
    {
        public override bool EhValido()
        {
            ValidationResult = new AtualizarContaFinanceiraValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class AtualizarContaFinanceiraValidator : AbstractValidator<AtualizarContaFinanceiraCommand>
    {
        public AtualizarContaFinanceiraValidator()
        {

        }
    }

}
