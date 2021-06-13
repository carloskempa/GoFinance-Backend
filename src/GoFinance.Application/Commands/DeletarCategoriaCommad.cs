using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class DeletarCategoriaCommad : Command
    {
        public DeletarCategoriaCommad(Guid categoriaId, Guid usuarioId)
        {
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;
        }

        public Guid CategoriaId { get; private set; }
        public Guid UsuarioId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new DeletarCategoriaValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class DeletarCategoriaValidator : AbstractValidator<DeletarCategoriaCommad>
    {
        public DeletarCategoriaValidator()
        {
            RuleFor(c => c.CategoriaId).NotEqual(Guid.Empty).WithMessage("Id da categoria inválido");
            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty).WithMessage("Id do Usuário inválido");
        }
    }

}
