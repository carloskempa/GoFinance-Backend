using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Events
{
    public class EnviarEmailResetarSenhaUsuarioEvent : Event
    {
        public Guid UsuarioId { get;private set; }
        public string UrlSite { get; private set; }
        public EnviarEmailResetarSenhaUsuarioEvent(Guid usuarioId, string urlSite)
        {
            AggregateId = usuarioId;
            UsuarioId = usuarioId;
            UrlSite = urlSite;
        }
    }
}
