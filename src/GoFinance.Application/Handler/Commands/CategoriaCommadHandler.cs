using GoFinance.Application.Commands;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.Messages;
using GoFinance.Domain.Core.Messages.CommonMessages.Notifications;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Commands
{
    public class CategoriaCommadHandler : IRequestHandler<AdicionarCategoriaCommand, bool>,
                                          IRequestHandler<AtualizarCategoriaCommand, bool>,
                                          IRequestHandler<DeletarCategoriaCommad, bool>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMovimentoRepository _movimentoRepository;

        public CategoriaCommadHandler(IUsuarioRepository usuarioRepository, IMediatorHandler mediatorHandler, IMovimentoRepository movimentoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _mediatorHandler = mediatorHandler;
            _movimentoRepository = movimentoRepository;
        }

        public async Task<bool> Handle(AdicionarCategoriaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var categoria = new Categoria(request.Nome, request.Codigo, true, request.UsuarioId);
            _usuarioRepository.Adicionar(categoria);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarCategoriaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var categoria = await _usuarioRepository.ObterCategoriaPorId(request.CategoriaId, request.UsuarioId);

            if (categoria == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Categoria não encontrada"));
                return false;
            }

            categoria.Atualizar(request.Nome, request.Codigo);
            _usuarioRepository.Atualizar(categoria);

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(DeletarCategoriaCommad request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var categoria = await _usuarioRepository.ObterCategoriaPorId(request.CategoriaId, request.UsuarioId);

            if (categoria == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Categoria não encontrada"));
                return false;
            }

            var contasPagar = await _movimentoRepository.ObterContaPagarPorCategoria(request.CategoriaId);

            if (!contasPagar.Any())
            {
                _usuarioRepository.Deletar(categoria);
            }
            else
            {
                categoria.Inativar();
                _usuarioRepository.Atualizar(categoria);
            }

            return await _usuarioRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido())
                return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
