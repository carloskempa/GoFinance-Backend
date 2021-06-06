using System;
using GoFinance.Domain.Core.DomainObjects;
using GoFinance.Domain.Enuns;

namespace GoFinance.Domain.Entities
{
    public class Parcela : Entity
    {
        public Parcela(string nomeDescritivo,int numeroParcela, decimal valor, DateTime dtCompetencia, DateTime dtVencimento)
        {
            NomeDescritivo = nomeDescritivo;
            NumeroParcela = numeroParcela;
            Valor = valor;
            DtCompetencia = dtCompetencia;
            DtVencimento = dtVencimento;
            StatusParcela = StatusParcela.Aberto;

            Validar();
        }
        protected Parcela() { }

        public string NomeDescritivo { get; private set; }
        public int NumeroParcela { get; private set; }
        public decimal Valor { get; private set; }
        public decimal? Desconto { get; private set; }
        public decimal? Multa { get; private set; }
        public decimal? Juros { get; private set; }
        public StatusParcela StatusParcela { get; private set; }
        public DateTime DtCompetencia { get; private set; }
        public DateTime? DtPagamento { get; private set; }
        public DateTime DtVencimento { get; private set; }
        public Guid? ContaFinanceiraId { get; private set; }
        public Guid ContaPagarId { get; private set; }
        public ContaFinanceira ContaFinanceira { get; private set; }
        public ContasPagar ContaPagar { get; private set; }

        public override void Validar()
        {
            Validacoes.ValidarSeIgual(Valor, 0, "O valor não pode ser zerado.");
            Validacoes.ValidarSeIgual(NumeroParcela, 0, "O número da parcela não pode ser zerado.");
        }

        public void PagarParcela(decimal valor, Guid contafinanceiraid, decimal? multa = null, decimal? juros = null, decimal? desconto = null)
        {
            Validacoes.ValidarSeIgual(valor, 0, "O valor não pode ser zerado.");
            Validacoes.ValidarSeDiferente(Valor, valor, "O valor de pagamento não pode ser maior ou menor que a parcela");

            Multa = multa;
            Juros = juros;
            Desconto = desconto;
            StatusParcela = StatusParcela.Pago;
            DtPagamento = DateTime.Now;
            ContaFinanceiraId = contafinanceiraid;
        }

        internal void AssociarConta(Guid contaPagarId)
        {
            ContaPagarId = contaPagarId;
        }
    }
}
