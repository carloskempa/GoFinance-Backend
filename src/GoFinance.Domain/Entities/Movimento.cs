using System;
using GoFinance.Domain.Core.DomainObjects;
using GoFinance.Domain.Enuns;

namespace GoFinance.Domain.Entities
{
    public class Movimento : Entity, IAggregateRoot
    {
        public Movimento(string nomeDescritivo, DateTime dtMovimento, string descricao, decimal valor, TipoMovimento tipoMovimento, Guid? contaPagarId, Guid? contaFinanceiraId)
        {
            NomeDescritivo = nomeDescritivo;
            DtMovimento = dtMovimento;
            Descricao = descricao;
            Valor = valor;
            TipoMovimento = tipoMovimento;
            ContaPagarId = contaPagarId;
            ContaFinanceiraId = contaFinanceiraId;

            Validar();
        }
        protected Movimento() { }

        public string NomeDescritivo { get; private set; }
        public DateTime DtMovimento { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public TipoMovimento TipoMovimento { get; private set; }
        public Guid? ContaPagarId { get; private set; }
        public Guid? ContaFinanceiraId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public ContasPagar ContasPagar { get; private set; }
        public ContaFinanceira ContaFinanceira { get; private set; }
        public Usuario Usuario { get; private set; }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(NomeDescritivo, "O nome descritivo não pode ser vazio.");
            Validacoes.ValidarSeNulo(DtMovimento, "A data de motimento não pode ser vazio.");
            Validacoes.ValidarSeIgual(Valor, 0, "O campo valor do Movimento não pode ser 0.");
        }


        public static class MovimentoFactory
        {
            public static Movimento Deposito(Guid contaFinanceira, decimal valor, Guid usuarioId)
            {
                var movimento = new Movimento
                {
                    ContaFinanceiraId = contaFinanceira,
                    Valor = valor,
                    DtMovimento = DateTime.Now,
                    TipoMovimento = TipoMovimento.Entrada,
                    UsuarioId = usuarioId
                };

                movimento.NomeDescritivo = $"Entrada | Depósito em conta {movimento.DtMovimento:dd/MM/yyyy}";
                movimento.Descricao = $"Depósito {valor}, às {movimento.DtMovimento:dd/MM/yyyy HH:mm:ss}";

                return movimento;
            }
            public static Movimento Pagamento(Parcela parcela, decimal valorTotal)
            {
                var movimento = new Movimento
                {
                    ContaFinanceiraId = parcela.ContaFinanceiraId,
                    Valor = valorTotal,
                    DtMovimento = DateTime.Now,
                    TipoMovimento = TipoMovimento.Saida,
                    UsuarioId = parcela.ContaPagar.UsuarioId
                };

                movimento.NomeDescritivo = $"Saída | Pagamento Parcela [{parcela.NomeDescritivo}]";
                movimento.Descricao = $"Pagamento {valorTotal} da parcela {parcela.NomeDescritivo}, às {movimento.DtMovimento:dd/MM/yyyy HH:mm:ss}";

                return movimento;
            }
            public static Movimento Saque(Guid contaFinanceiraId, decimal valor, Guid usuarioId)
            {
                var movimento = new Movimento
                {
                    ContaFinanceiraId = contaFinanceiraId,
                    Valor = valor,
                    DtMovimento = DateTime.Now,
                    TipoMovimento = TipoMovimento.Saida,
                    UsuarioId = usuarioId
                };

                movimento.NomeDescritivo = $"Saída | Saque da Conta Financeira";
                movimento.Descricao = $"Saque {valor}, às {movimento.DtMovimento:dd/MM/yyyy HH:mm:ss}";

                return movimento;
            }
        }
    }
}
