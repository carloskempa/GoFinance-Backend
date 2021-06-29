using GoFinance.Application.Commands;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Commands
{
    public class ContasPagarCommandHandler : HandlerBase, IRequestHandler<AdicionarContasPagarCommand, bool>
    {
        private readonly IMovimentoRepository _movimentoRepository;

        public ContasPagarCommandHandler(IMediatorHandler mediatorHandler, IMovimentoRepository movimentoRepository) : base(mediatorHandler)
        {
            _movimentoRepository = movimentoRepository;
        }

        public async Task<bool> Handle(AdicionarContasPagarCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var numeroParcela = request.NumeroParcelas == 0 ? 1 : request.NumeroParcelas;
            var contaPagar = new ContasPagar(request.Nome, numeroParcela, request.ValorTotal, request.Observacoes, request.CategoriaId, request.UsuarioId, request.FornecedorId);

            var parcelas = new List<Parcela>();
            var dataVencimento = request.PrimeiroVencimento;
            var valorParcela = request.ValorTotal / numeroParcela;

            for (int i = 1; i < numeroParcela; i++)
            {
                var nomeParcela = $"{request.Nome.Substring(1, 10)} {i}/{numeroParcela}";
                var parcela = new Parcela(nomeParcela, i, valorParcela, dataVencimento.AddMonths(i));

                parcelas.Add(parcela);
                contaPagar.AdicionarParcela(parcela);
            }

            _movimentoRepository.Adicionar(contaPagar);
            _movimentoRepository.AdicionarTodos(parcelas);

            return await Commit(_movimentoRepository.UnitOfWork);
        }
    }
}
