using AutoMapper;
using GoFinance.Application.Commands;
using GoFinance.Application.Dtos;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoFinance.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        public FornecedorController(INotificationHandler<DomainNotification> notifications,
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
        public async Task<ActionResult<RetornoPadrao<FornecedorViewModel>>> Cadastrar([FromBody] FornecedorViewModel model)
        {
            try
            {
                var command = new AdicionarFornecedorCommand(model.Nome, model.CnpjCpf, model.UrlSite, model.Descricao, model.Ativo, UsuarioIdLogado);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<FornecedorViewModel>(ObterMensagensErro);

                return Success(model);
            }
            catch (Exception ex)
            {
                return Error<FornecedorViewModel>(ex.Message);
            }
        }

        [HttpPut("{id}/atualizar")]
        public async Task<ActionResult<RetornoPadrao<FornecedorViewModel>>> Atualizar(Guid id, [FromBody] FornecedorViewModel model)
        {
            try
            {
                var command = new AtualizarFornecedorCommand(id, model.Nome, model.CnpjCpf, model.UrlSite, model.Descricao, UsuarioIdLogado);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<FornecedorViewModel>(ObterMensagensErro);

                return Success(model);
            }
            catch (Exception ex)
            {
                return Error<FornecedorViewModel>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeletarFornecedorCommand(id, UsuarioIdLogado);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<FornecedorViewModel>(ObterMensagensErro);

                return Ok();
            }
            catch (Exception ex)
            {
                return Error<FornecedorViewModel>(ex.Message);
            }
        }

    }
}
