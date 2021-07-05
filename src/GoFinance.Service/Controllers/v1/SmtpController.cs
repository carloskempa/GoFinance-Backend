using AutoMapper;
using GoFinance.Application.Commands;
using GoFinance.Application.Dtos;
using GoFinance.Application.ViewModels;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoFinance.Service.Controllers.v1
{

    [Route("api/[controller]")]
    [ApiController]
    public class SmtpController : ControllerBase
    {
        public SmtpController(INotificationHandler<DomainNotification> notifications,
             IMediatorHandler mediatorHandler, IMapper mapper) : base(notifications, mediatorHandler, mapper)
        {

        }

        [HttpGet]
        public ActionResult List()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<RetornoPadrao<SmtpViewModel>>> Cadastrar([FromBody] SmtpViewModel model)
        {
            try
            {
                var command = new AdicionarSmtpCommand(model.Email, model.Mascara, model.Host, model.Porta, model.Usuario, model.Senha, model.SSL, model.Ativo);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<SmtpViewModel>(ObterMensagensErro);

                return Success(model);
            }
            catch (Exception ex)
            {
                return Error<SmtpViewModel>(ex.Message);
            }
        }

        [HttpPut("{id}/atualizar")]
        public async Task<ActionResult<RetornoPadrao<SmtpViewModel>>> Atualizar(Guid id, [FromBody] SmtpViewModel model)
        {
            try
            {
                var command = new AtualizarSmtpCommand(id, model.Email, model.Mascara, model.Host, model.Porta, model.Usuario, model.Senha, model.SSL, model.Ativo);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<SmtpViewModel>(ObterMensagensErro);

                return Success(model);
            }
            catch (Exception ex)
            {
                return Error<SmtpViewModel>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeletarSmtpCommand(id);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<SmtpViewModel>(ObterMensagensErro);

                return Ok();
            }
            catch (Exception ex)
            {
                return Error<SmtpViewModel>(ex.Message);
            }
        }


    }
}
