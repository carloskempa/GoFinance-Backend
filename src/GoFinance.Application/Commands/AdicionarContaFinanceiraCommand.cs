using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class AdicionarContaFinanceiraCommand : Command
    {
        public AdicionarContaFinanceiraCommand(string nome, string banco, string descricao, decimal saldo, Guid usuarioId)
        {
            Nome = nome;
            Banco = banco;
            Descricao = descricao;
            Saldo = saldo;
            UsuarioId = usuarioId;
        }

        public string Nome { get; private set; }
        public string Banco { get; private set; }
        public string Descricao { get; private set; }
        public decimal Saldo { get; private set; }
        public Guid UsuarioId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarContaFinanceiraValitator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class AdicionarContaFinanceiraValitator : AbstractValidator<AdicionarContaFinanceiraCommand>
    {
        public AdicionarContaFinanceiraValitator()
        {

        }
    }

}
