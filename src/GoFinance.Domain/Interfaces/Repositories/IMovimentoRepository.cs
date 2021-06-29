using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoFinance.Domain.Core.Data;
using GoFinance.Domain.Entities;

namespace GoFinance.Domain.Interfaces.Repositories
{
    public interface IMovimentoRepository : IRepository<Movimento>
    {
        //Movimento
        IQueryable<Movimento> ObterMovimentos();
        Task<IEnumerable<Movimento>> ObterTodos(Guid usuarioId);
        Task<Movimento> ObterPorId(Guid id);
        void Adicionar(Movimento movimento);
        void Atualizar(Movimento movimento);

        //ContaPagar
        IQueryable<ContasPagar> ObterContasPagar();
        Task<IEnumerable<ContasPagar>> ObterTodosContaPagar(Guid usuarioId);
        Task<IEnumerable<ContasPagar>> ObterContaPagarPorCategoria(Guid categoriaId);
        Task<ContasPagar> ObterContaPagarPorId(Guid id);
        void Adicionar(ContasPagar contasPagar);
        void Atualizar(ContasPagar contasPagar);
        void Deletar(ContasPagar contasPagar);
        void DeletarTodos(IEnumerable<ContasPagar> contasPagar);

        //Parcela
        IQueryable<Parcela> ObterParcelas();
        Task<Parcela> ObterPorParcelaId(Guid id);
        void Adicionar(Parcela parcela);
        void AdicionarTodos(List<Parcela> parcelas);
        void Atualizar(Parcela parcela);
        void DeletarTodos(List<Parcela> parcelas);
    }
}
