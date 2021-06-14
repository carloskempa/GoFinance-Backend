using FluentValidation;
using GoFinance.Application.Commands;
using System;

namespace GoFinance.Application.Validators
{
    public class AdicionarContaFinanceiraValitator : AbstractValidator<AdicionarContaFinanceiraCommand>
    {
        public AdicionarContaFinanceiraValitator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("O campo Nome da Conta não pode estar vazio")
                                .MaximumLength(100).WithMessage("O campo nome da Conta não pode ter mais de 100 caracteres");

            RuleFor(c => c.Banco).NotEmpty().WithMessage("O campo Nome do Banco não pode estar vazio")
                                 .Length(1, 200).WithMessage("O campo nome do Banco não pode ter mais de 200 caracteres");

            RuleFor(c => c.Descricao).Length(1, 500).WithMessage("O campo descrição não pode ter mais de 500 caracteres");

            RuleFor(c => c.Saldo).NotEmpty().WithMessage("O campo saldo não pode estar vazio");

            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty) .WithMessage("Id do usuario inválido");
        }
    }

}
