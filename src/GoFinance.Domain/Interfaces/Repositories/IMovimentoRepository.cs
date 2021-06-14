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
        Task<IEnumerable<Movimento>> ObterTodos(Guid usuarioId);
        Task<Movimento> ObterPorId(Guid id);
        void Adicionar(Movimento movimento);
        void Atualizar(Movimento movimento);


        //ContaPagar
        Task<IEnumerable<ContasPagar>> ObterTodosContaPagar(Guid usuarioId);
        Task<IEnumerable<ContasPagar>> ObterContaPagarPorCategoria(Guid categoriaId);
        IQueryable<ContasPagar> ObterContaPagar();
        Task<ContasPagar> ObterContaPagarPorId(Guid id);
        void Adicionar(ContasPagar contasPagar);
        void Atualizar(ContasPagar contasPagar);
        void Deletar(ContasPagar contasPagar);
        void DeletarTodos(IEnumerable<ContasPagar> contasPagar);

        //Parcela
        void Adicionar(Parcela parcela);
        void AdicionarTodos(IEnumerable<Parcela> parcelas);
        void DeletarTodos(IEnumerable<Parcela> parcelas);
    }
}
