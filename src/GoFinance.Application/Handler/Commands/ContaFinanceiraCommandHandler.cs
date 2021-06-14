using GoFinance.Application.Commands;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Commands
{
    public class ContaFinanceiraCommandHandler : HandlerBase, IRequestHandler<AdicionarContaFinanceiraCommand, bool>,
                                                              IRequestHandler<AtualizarContaFinanceiraCommand, bool>,
                                                              IRequestHandler<DeletarContaFinanceiraCommand, bool>
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public ContaFinanceiraCommandHandler(IUsuarioRepository usuarioRepository, IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(AdicionarContaFinanceiraCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;


            return await Commit(_usuarioRepository.UnitOfWork);
        }

        public async Task<bool> Handle(AtualizarContaFinanceiraCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;


            return await Commit(_usuarioRepository.UnitOfWork);
        }

        public async Task<bool> Handle(DeletarContaFinanceiraCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;


            return await Commit(_usuarioRepository.UnitOfWork);
        }
    }
}
