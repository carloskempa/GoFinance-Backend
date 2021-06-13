using GoFinance.Domain.Core.Messages;
using GoFinance.Domain.Entities;
using System;

namespace GoFinance.Application.Events
{
    public class EnviarEmailResetarSenhaUsuarioEvent : Event
    {
        public Usuario Usuario { get;private set; }
        public string UrlSite { get; private set; }
        public EnviarEmailResetarSenhaUsuarioEvent(Usuario usuario, string urlSite)
        {
            AggregateId = usuario.Id;
            Usuario = usuario;
            UrlSite = urlSite;
        }
    }
}
