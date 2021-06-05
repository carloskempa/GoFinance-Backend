using System;
using System.Collections.Generic;
using GoFinance.Domain.Core.DomainObjects;

namespace GoFinance.Domain.Entities
{
    public class ContaFinanceira : Entity, IAggregateRoot
    {
        public ContaFinanceira(string nome, string banco, string descricao, decimal saldo, Guid usuarioId)
        {
            Nome = nome;
            Banco = banco;
            Descricao = descricao;
            Saldo = saldo;
            UsuarioId = usuarioId;

            Validar();
        }
        protected ContaFinanceira() { }

        public string Nome { get; private set; }
        public string Banco { get; private set; }
        public string Descricao { get; private set; }
        public decimal Saldo { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public bool Ativo { get; private set; }
        public ICollection<Movimento> Movimentos { get; private set; }
        public ICollection<Parcela> Parcelas { get; private set; }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo do Nome da canta deve ser preenchida.");
            Validacoes.ValidarSeVazio(Banco, "O campo do Banco deve ser preenchida.");
            Validacoes.ValidarSeMenorQue(Saldo, 0, "O campo do Saldo nao deve ser preenchida com valor negativo.");
            Validacoes.ValidarSeIgual(UsuarioId, Guid.Empty, "O campo do UsuarioId deve ser preenchida.");
        }


        public void AdicionarSaldo(decimal valor)
        {
            Validacoes.ValidarSeIgual(valor, 0, "O valor não deve ser negativo.");
            Saldo += valor;
        }

        public void DebitarSaldo(decimal valor)
        {
            Validacoes.ValidarSeIgual(valor, 0, "O valor não deve ser negativo.");
            Saldo -= valor;
        }
    }
}
