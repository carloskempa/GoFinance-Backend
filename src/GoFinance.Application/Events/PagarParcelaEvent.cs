using GoFinance.Domain.Core.Messages;
using GoFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFinance.Application.Events
{
    public class PagarParcelaEvent : Event
    {
        public Parcela Parcela { get; set; }
        public decimal Valor { get; private set; }

        public PagarParcelaEvent(Parcela parcela, decimal valor)
        {
            Parcela = parcela;
            Valor = valor;
        }
    }
}
