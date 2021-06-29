using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;
using System.Collections.Generic;

namespace GoFinance.Application.Commands
{
    public class AdicionarContasPagarCommand : Command
    {
        public AdicionarContasPagarCommand(string nome, int numeroParcelas, decimal valorTotal, string observacoes, Guid categoriaId, Guid usuarioId, Guid fornecedorId)
        {
            Nome = nome;
            NumeroParcelas = numeroParcelas;
            ValorTotal = valorTotal;
            Observacoes = observacoes;
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;
            FornecedorId = fornecedorId;
        }

        public string Nome { get; private set; }
        public int NumeroParcelas { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string Observacoes { get; private set; }
        public DateTime PrimeiroVencimento { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid? FornecedorId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarContasPagarValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarContasPagarValidator : AbstractValidator<AdicionarContasPagarCommand>
    {
        public AdicionarContasPagarValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("O campo Nome não pode estar vazio");
            RuleFor(c => c.ValorTotal).GreaterThan(0).WithMessage("O valor tem que ser maior que 0");
            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty).WithMessage("Id do usuario inválido");
            RuleFor(c => c.CategoriaId).NotEqual(Guid.Empty).WithMessage("Id da categoria inválido");
        }
    }
}
