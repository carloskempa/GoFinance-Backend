using GoFinance.Application.Commands;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Commands
{
    public class ContaFinanceiraCommandHandler : HandlerBase, IRequestHandler<AdicionarContaFinanceiraCommand, bool>,
                                                              IRequestHandler<AtualizarContaFinanceiraCommand, bool>,
                                                              IRequestHandler<DeletarContaFinanceiraCommand, bool>
    {

        private readonly IContaFinanceiraRepository  _contaFinanceiraRepository;
        private readonly IMovimentoRepository _movimentoRepository;

        public ContaFinanceiraCommandHandler(IContaFinanceiraRepository contaFinanceiraRepository, IMovimentoRepository movimentoRepository, IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _contaFinanceiraRepository = contaFinanceiraRepository;
            _movimentoRepository = movimentoRepository;
        }

        public async Task<bool> Handle(AdicionarContaFinanceiraCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var contaFinanceira = new ContaFinanceira(request.Nome, request.Banco, request.Descricao, request.Saldo,true, request.UsuarioId);
            _contaFinanceiraRepository.Adicionar(contaFinanceira);

            return await Commit(_contaFinanceiraRepository.UnitOfWork);
        }

        public async Task<bool> Handle(AtualizarContaFinanceiraCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var contaFinanceira = await _contaFinanceiraRepository.ObterPorId(request.ContaFinanceiraId, request.UsuarioId);

            if(contaFinanceira == null)
            {
                await AdicionarEventError(request.MessageType, "Conta Financeira não encontrado");
                return false;
            }

            contaFinanceira.Atualizar(request.Nome, request.Banco, request.Descricao, request.Saldo, request.Ativo);
            _contaFinanceiraRepository.Atualizar(contaFinanceira);

            return await Commit(_contaFinanceiraRepository.UnitOfWork);
        }

        public async Task<bool> Handle(DeletarContaFinanceiraCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var contaFinanceira = await _contaFinanceiraRepository.ObterPorId(request.ContaFinanceiraId, request.UsuarioId);

            if (contaFinanceira == null)
            {
                await AdicionarEventError(request.MessageType, "Conta Financeira não encontrado");
                return false;
            }

            var movimentos = await _movimentoRepository.ObterMovimentos().Where(c => c.ContaFinanceiraId == request.ContaFinanceiraId).ToListAsync();
            var parcelas = await _movimentoRepository.ObterParcelas().Where(c => c.ContaFinanceiraId == request.ContaFinanceiraId).ToListAsync();

            if (movimentos.Any())
                contaFinanceira.Desativar();

            if (parcelas.Any())
                contaFinanceira.Desativar();


            if (contaFinanceira.Ativo)
                _contaFinanceiraRepository.Deletar(contaFinanceira);
            else
                _contaFinanceiraRepository.Atualizar(contaFinanceira);


            return await Commit(_contaFinanceiraRepository.UnitOfWork);
        }
    }
}
