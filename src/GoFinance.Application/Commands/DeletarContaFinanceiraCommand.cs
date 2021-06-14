using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoFinance.Application.Commands
{
    public class DeletarContaFinanceiraCommand : Command
    {
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

        }
    }
}
