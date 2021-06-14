using System;
using FluentValidation;
using GoFinance.Domain.Core.Messages;

namespace GoFinance.Application.Commands
{
    public class AtualizarContaFinanceiraCommand : Command
    {
        public AtualizarContaFinanceiraCommand(Guid contaFinanceiraId, string nome, string banco, string descricao, decimal saldo, bool ativo, Guid usuarioId)
        {
            ContaFinanceiraId = contaFinanceiraId;
            Nome = nome;
            Banco = banco;
            Descricao = descricao;
            Saldo = saldo;
            Ativo = ativo;
            UsuarioId = usuarioId;
        }

        public Guid ContaFinanceiraId { get; protected set; }
        public string Nome { get; private set; }
        public string Banco { get; private set; }
        public string Descricao { get; private set; }
        public decimal Saldo { get; private set; }
        public bool Ativo { get; private set; }
        public Guid UsuarioId { get; private set; }

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
            RuleFor(c => c.ContaFinanceiraId).NotEqual(Guid.Empty).WithMessage("Id da conta financeira é inválido");

            RuleFor(c => c.Nome).NotEmpty().WithMessage("O campo Nome da Conta não pode estar vazio")
                                .MaximumLength(100).WithMessage("O campo nome da Conta não pode ter mais de 100 caracteres");

            RuleFor(c => c.Banco).NotEmpty().WithMessage("O campo Nome do Banco não pode estar vazio")
                                 .Length(1, 200).WithMessage("O campo nome do Banco não pode ter mais de 200 caracteres");

            RuleFor(c => c.Descricao).Length(1, 500).WithMessage("O campo descrição não pode ter mais de 500 caracteres");

            RuleFor(c => c.Saldo).NotEmpty().WithMessage("O campo saldo não pode estar vazio");

            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty).WithMessage("Id do usuario inválido");
        }
    }

}
