using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GoFinance.Data.Context;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Core.Data;
using Microsoft.EntityFrameworkCore;
using GoFinance.Domain.Interfaces.Repositories;

namespace GoFinance.Data.Repository
{
    public class ContaFinanceiraRepository : IContaFinanceiraRepository
    {
        private readonly FinanceContext _context;

        public ContaFinanceiraRepository(FinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ContaFinanceira> ObterPorId(Guid id, Guid usuarioId)
        {
            return await _context.ContasFinanceiras.FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);
        }

        public async Task<IEnumerable<ContaFinanceira>> ObterTodos(Guid usuarioId)
        {
            return await _context.ContasFinanceiras.AsNoTracking().Where(c => c.UsuarioId == usuarioId).ToListAsync();
        }
        
        public async Task<ContaFinanceira> ObterPorId(Guid id)
        {
            return await _context.ContasFinanceiras.FindAsync(id);
        }

        public void Adicionar(ContaFinanceira contaFinanceira)
        {
            _context.ContasFinanceiras.Add(contaFinanceira);
        }

        public void Atualizar(ContaFinanceira contaFinanceira)
        {
            _context.ContasFinanceiras.Update(contaFinanceira);
        }

        public void Deletar(ContaFinanceira contaFinanceira)
        {
            _context.ContasFinanceiras.Remove(contaFinanceira);
        }
       
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
