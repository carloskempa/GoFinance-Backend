using GoFinance.Application.Events;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static GoFinance.Domain.Entities.Movimento;

namespace GoFinance.Application.Handler.Event
{
    public class MovimentoEventHandler : INotificationHandler<AdicionarMovimentoFinanceiroEvent>, INotificationHandler<SaquarMovimentoEvent>
    {
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentoEventHandler(IMovimentoRepository movimentoRepository)
        {
            _movimentoRepository = movimentoRepository;
        }

        public async Task Handle(AdicionarMovimentoFinanceiroEvent notification, CancellationToken cancellationToken)
        {
            var movimento = MovimentoFactory.Deposito(notification.ContaFinanceiraId, notification.Valor, notification.UsuarioId);
            _movimentoRepository.Adicionar(movimento);

            await _movimentoRepository.UnitOfWork.Commit();
        }

        public async Task Handle(SaquarMovimentoEvent notification, CancellationToken cancellationToken)
        {
            var movimento = MovimentoFactory.Saque(notification.ContaFinanceiraId, notification.Valor, notification.UsuarioId);
            _movimentoRepository.Adicionar(movimento);

            await _movimentoRepository.UnitOfWork.Commit();
        }
    }
}
