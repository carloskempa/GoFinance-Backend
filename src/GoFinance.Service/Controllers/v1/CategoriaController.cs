using AutoMapper;
using GoFinance.Application.Commands;
using GoFinance.Application.Dtos;
using GoFinance.Application.ViewModels;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using GoFinance.Domain.Interfaces.Queries;
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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaQueries _categoriaQueries;
        public CategoriaController(INotificationHandler<DomainNotification> notifications,
           IMediatorHandler mediatorHandler, ICategoriaQueries categoriaQueries, IMapper mapper) : base(notifications, mediatorHandler, mapper)
        {
            _categoriaQueries = categoriaQueries;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RetornoPadrao<CategoriaDto>>> Get(Guid id)
        {
            try
            {
                var categoria = await _categoriaQueries.ObterPorId(id, UsuarioIdLogado);
                return Success<CategoriaDto>();
            }
            catch (Exception ex)
            {
                return Error<CategoriaViewModel>(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult<RetornoPadrao<CategoriaViewModel>>> Cadastrar([FromBody] CategoriaViewModel categoria)
        {
            try
            {
                var command = new AdicionarCategoriaCommand(categoria.Nome, categoria.Codigo, true, UsuarioIdLogado);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<CategoriaViewModel>(ObterMensagensErro);

                return Success(categoria);
            }
            catch (Exception ex)
            {
                return Error<CategoriaViewModel>(ex.Message);
            }
        }


        [HttpPut("{id}/update")]
        public async Task<ActionResult<RetornoPadrao<CategoriaViewModel>>> Atualizar(Guid id, [FromBody] CategoriaViewModel categoria)
        {
            try
            {
                var command = new AtualizarCategoriaCommand(categoria.Nome, categoria.Codigo, id, UsuarioIdLogado);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<CategoriaViewModel>(ObterMensagensErro);

                return Success(categoria);
            }
            catch (Exception ex)
            {
                return Error<CategoriaViewModel>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                var command = new DeletarCategoriaCommad(id, UsuarioIdLogado);
                var result = await _mediatorHandler.EnviarComando(command);

                if (!OperacaoValida())
                    return Error<dynamic>(ObterMensagensErro);

                return Success<dynamic>();
            }
            catch (Exception ex)
            {
                return Error<CategoriaViewModel>(ex.Message);
            }
        }

    }
}
