using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoFinance.Domain.Core.Data;
using GoFinance.Domain.Entities;

namespace GoFinance.Domain.Interfaces.Repositories
{
    public interface IContaFinanceiraRepository : IRepository<ContaFinanceira>
    {
        Task<IEnumerable<ContaFinanceira>> ObterTodos();
        Task<IEnumerable<ContaFinanceira>> ObterTodos(Guid usuarioId);
        Task<ContaFinanceira> ObterPorId(Guid id, Guid usuarioId);

        void Adicionar(ContaFinanceira contaFinanceira);
        void Atualizar(ContaFinanceira contaFinanceira);
        void Deletar(ContaFinanceira contaFinanceira);
    }
}
