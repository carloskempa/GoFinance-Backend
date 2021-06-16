using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Events
{
    public class SaquarMovimentoEvent : Event
    {
        public Guid ContaFinanceiraId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public decimal Valor { get; private set; }

        public SaquarMovimentoEvent(Guid contaFinanceiraId, Guid usuarioId, decimal valor)
        {
            AggregateId = contaFinanceiraId;
            ContaFinanceiraId = contaFinanceiraId;
            UsuarioId = usuarioId;
            Valor = valor;
        }
    }
}
