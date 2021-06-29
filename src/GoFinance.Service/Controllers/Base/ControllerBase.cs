using AutoMapper;
using GoFinance.Application.Dtos;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GoFinance.Service.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediatorHandler;
        protected readonly IMapper _mapper;

        protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }
        protected Guid UsuarioIdLogado => ObterUsuarioLogado();
        private Guid ObterUsuarioLogado()
        {
            var dados = User as ClaimsPrincipal;
            var idUsuario = dados.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
            return Guid.Parse(idUsuario);
        }
        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }
        protected List<string> ObterMensagensErro => ObterMensagensErros();

        private List<string> ObterMensagensErros()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }
        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }


        protected ObjectResult Success<TData>() where TData : new()
        {
            var retorno = new RetornoPadrao<TData>
            {
                Sucesso = true,
                Data = default
            };

            return Ok(retorno);
        }
        protected ObjectResult Success<TData>(TData data) where TData : new()
        {
            var retorno = new RetornoPadrao<TData>
            {
                Sucesso = true,
                Data = data
            };

            return Ok(retorno);
        }
        protected ObjectResult Success<TData>(List<string> mensagens) where TData : new()
        {
            var retorno = new RetornoPadrao<TData>
            {
                Sucesso = true,
                Data = default,
                Mensagens = mensagens
            };

            return Ok(retorno);
        }
        protected ObjectResult Success<TData>(TData data, List<string> mensagens) where TData : new()
        {
            var retorno = new RetornoPadrao<TData>
            {
                Sucesso = true,
                Data = data,
                Mensagens = mensagens
            };

            return Ok(retorno);
        }


        protected ObjectResult Error<TData>() where TData : new()
        {
            var retorno = new RetornoPadrao<TData>
            {
                Sucesso = false,
                Data = default
            };

            return BadRequest(retorno);
        }
        protected ObjectResult Error<TData>(string mensagem) where TData : new()
        {
            var retorno = new RetornoPadrao<TData>
            {
                Sucesso = false,
                Mensagens = new List<string>() { mensagem },
                Data = default
            };

            return BadRequest(retorno);
        }
        protected ObjectResult Error<TData>(List<string> mensagens) where TData : new()
        {
            var retorno = new RetornoPadrao<TData>
            {
                Sucesso = false,
                Mensagens = mensagens,
                Data = default
            };
            return BadRequest(retorno);
        }
    }

}
