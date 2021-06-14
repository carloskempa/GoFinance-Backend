using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class DeletarFornecedorCommand : Command
    {
        public DeletarFornecedorCommand(Guid fornecedorId, Guid usuarioId)
        {
            FornecedorId = fornecedorId;
            UsuarioId = usuarioId;
        }

        public Guid FornecedorId { get; private set; }
        public Guid UsuarioId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new DeletarFornecedorValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DeletarFornecedorValidator : AbstractValidator<DeletarFornecedorCommand>
    {
        public DeletarFornecedorValidator()
        {
            RuleFor(c => c.FornecedorId).NotEqual(Guid.Empty).WithMessage("Id do Fornecedor inválido");
            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty).WithMessage("Id do usuario inválido");
        }
    }
 }
