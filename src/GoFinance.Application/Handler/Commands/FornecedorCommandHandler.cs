using GoFinance.Application.Commands;
using GoFinance.Domain.Core.Communication.Mediator;
using GoFinance.Domain.Core.ValuesObjects;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoFinance.Application.Handler.Commands
{
    public class FornecedorCommandHandler : HandlerBase, IRequestHandler<AdicionarFornecedorCommand, bool>,
                                                         IRequestHandler<AtualizarFornecedorCommand, bool>,
                                                         IRequestHandler<DeletarFornecedorCommand, bool>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        public FornecedorCommandHandler(IUsuarioRepository usuarioRepository, IMovimentoRepository movimentoRepository, IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _usuarioRepository = usuarioRepository;
            _movimentoRepository = movimentoRepository;
        }
        public async Task<bool> Handle(AdicionarFornecedorCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var fornecedor = new Fornecedor(request.Nome, request.UrlSite, request.Descricao, true, request.UsuarioId, new CnpjCpf(request.CnpjCpf));
            _usuarioRepository.Adicionar(fornecedor);

            return await Commit(_usuarioRepository.UnitOfWork);
        }

        public async Task<bool> Handle(AtualizarFornecedorCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var fornecedor = await _usuarioRepository.ObterFornecedorPorId(request.FornecedorId, request.UsuarioId);

            if (fornecedor == null)
            {
                await AdicionarEventError(request.MessageType, "Fornecedor não encontrado");
                return false;
            }

            fornecedor.Atualizar(new CnpjCpf(request.CnpjCpf), request.Nome, request.UrlSite, request.Descricao);
            _usuarioRepository.Atualizar(fornecedor);

            return await Commit(_usuarioRepository.UnitOfWork);
        }
       
        public async Task<bool> Handle(DeletarFornecedorCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var fornecedor = await _usuarioRepository.ObterFornecedorPorId(request.FornecedorId, request.UsuarioId);

            if (fornecedor == null)
            {
                await AdicionarEventError(request.MessageType, "Fornecedor não encontrado");
                return false;
            }

            var contasPagar = await _movimentoRepository.ObterContaPagar().Where(c => c.FornecedorId == request.FornecedorId).ToListAsync();
            fornecedor.Desativar();

            if (contasPagar.Any())
                _usuarioRepository.Atualizar(fornecedor);
            else
                _usuarioRepository.Deletar(fornecedor);

            return await Commit(_usuarioRepository.UnitOfWork);
        }
    }
}
