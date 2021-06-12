using FluentValidation;
using GoFinance.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoFinance.Application.Validators
{
    public class AdicionarCategoriaValidator : AbstractValidator<AdicionarCategoriaCommand>
    {

        public AdicionarCategoriaValidator()
        {
            RuleFor(c => c.Nome).NotNull()
                                .NotEmpty()
                                .WithMessage("O campo Nome da Categoria não pode estar vazio");

            RuleFor(c => c.Codigo).LessThanOrEqualTo(0)
                                  .WithMessage("O campo Código da Categoria não pode estar vazio");

            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty)
                                     .WithMessage("Id do usuario inválido");
        }
    }
}
