using GoFinance.Domain.Core.Messages;
using System;
using System.Collections.Generic;

namespace GoFinance.Domain.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime DtCadastro { get; set; }

        private List<Event> _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

        public Entity()
        {
            Id = new Guid();
        }

        public void AdicionarEvento(Event evento)
        {
            _notificacoes = _notificacoes ?? new List<Event>();
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event eventItem)
        {
            _notificacoes?.Remove(eventItem);
        }

        public void LimparEventos()
        {
            _notificacoes?.Clear();
        }


        public virtual void Validar()
        {
            throw new NotImplementedException();
        }

    }
}
