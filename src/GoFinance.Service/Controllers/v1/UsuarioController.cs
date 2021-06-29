using AutoMapper;
using GoFinance.Application.Commands;
using GoFinance.Application.Dtos;
using GoFinance.Application.ViewModels;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoFinance.Service.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(INotificationHandler<DomainNotification> notifications, IMapper mapper,
        IMediatorHandler mediatorHandler, IUsuarioRepository usuarioRepository) : base(notifications, mediatorHandler, mapper)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("logar")]
        public async Task<ActionResult<UsuarioLogadoDto>> Logar([FromBody] LogarViewModels logarViewModels)
        {
            try
            {
                var command = new AuthenticarUsuarioCommand(logarViewModels.Login, logarViewModels.Senha);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<UsuarioLogadoDto>(ObterMensagensErro);

                var usuario = _mapper.Map<UsuarioLogadoDto>(await _usuarioRepository.ObterPorLogin(logarViewModels.Login));


                return Success(usuario);
            }
            catch (Exception ex)
            {
                return Error<UsuarioLogadoDto>(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<RetornoPadrao<UsuarioViewModel>>> Cadastrar([FromBody] UsuarioViewModel model)
        {
            try
            {
                var command = new AdicionarUsuarioCommand(model.Nome, model.Login, model.Senha, model.Email, false);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<UsuarioLogadoDto>(ObterMensagensErro);

                return Success(model);
            }
            catch (Exception ex)
            {
                return Error<UsuarioViewModel>(ex.Message);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<RetornoPadrao<UsuarioViewModel>>> Atualizar([FromBody] UsuarioViewModel model)
        {
            try
            {
                var command = new AtualizarUsuarioCommand(UsuarioIdLogado, model.Nome, model.Login, model.Email, model.Ativo);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<UsuarioViewModel>(ObterMensagensErro);

                return Success(model);
            }
            catch (Exception ex)
            {
                return Error<UsuarioViewModel>(ex.Message);
            }
        }


    }
}
