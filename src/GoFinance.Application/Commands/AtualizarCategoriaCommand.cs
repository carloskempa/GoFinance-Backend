using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class AtualizarCategoriaCommand : Command
    {
        public AtualizarCategoriaCommand(string nome, int codigo, Guid categoriaId, Guid usuarioId)
        {
            Nome = nome;
            Codigo = codigo;
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;
        }

        public Guid CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public int Codigo { get; private set; }
        public Guid UsuarioId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarCategoriaValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }


    public class AtualizarCategoriaValidator : AbstractValidator<AtualizarCategoriaCommand>
    {
        public AtualizarCategoriaValidator()
        {
            RuleFor(c => c.CategoriaId).NotEqual(Guid.Empty).WithMessage("Id da categoria é inválido");
            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty).WithMessage("Id do usuário é inválido");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome da Categoria não pode estar vazio");
            RuleFor(c => c.Codigo).LessThanOrEqualTo(0).WithMessage("O campo Código da Categoria não pode estar vazio");
        }
    }
}
