using GoFinance.Application.Commands;
using GoFinance.Application.Events;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Commands
{
    public class ParcelaCommandHandler : HandlerBase, IRequestHandler<PagarParcelaCommand, bool>
    {
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IContaFinanceiraRepository _contaFinanceiraRepository;

        public ParcelaCommandHandler(IMediatorHandler mediatorHandler, IMovimentoRepository movimentoRepository, IContaFinanceiraRepository contaFinanceiraRepository) : base(mediatorHandler)
        {
            _movimentoRepository = movimentoRepository;
            _contaFinanceiraRepository = contaFinanceiraRepository;
        }

        public async Task<bool> Handle(PagarParcelaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var parcela = await _movimentoRepository.ObterPorParcelaId(request.ContaFinanceiraId);
            var valorTotal =  (request.Valor + request.Multa + request.Juros) - request.Desconto;

            if (parcela == null)
            {
                await AdicionarEventError(request.MessageType, "Parcela não encontrado");
                return false;
            }

            var contaFinanceira = await _contaFinanceiraRepository.ObterPorId(request.ContaFinanceiraId);

            if (parcela == null)
            {
                await AdicionarEventError(request.MessageType, "Conta Financeira não encontrada");
                return false;
            }

            if (valorTotal > contaFinanceira.Saldo)
            {
                await AdicionarEventError(request.MessageType, "Saldo insuficiente");
                return false;
            }

            parcela.PagarParcela(request.Valor, request.ContaFinanceiraId, request.Multa, request.Juros, request.Desconto);
            parcela.AdicionarEvento(new PagarParcelaEvent(parcela, valorTotal));

            _movimentoRepository.Atualizar(parcela);

            return await Commit(_movimentoRepository.UnitOfWork);
        }
    }
}
