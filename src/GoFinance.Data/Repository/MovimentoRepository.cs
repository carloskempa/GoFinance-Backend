using GoFinance.Data.Context;
using GoFinance.Domain.Core.Data;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoFinance.Data.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly FinanceContext _context;

        public MovimentoRepository(FinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        #region Movimento


        public async Task<Movimento> ObterPorId(Guid id)
        {
            return await _context.Movimentos.FindAsync(id);
        }

        public async Task<IEnumerable<Movimento>> ObterTodos(Guid usuarioId)
        {
            return await _context.Movimentos.AsNoTracking().Where(c => c.UsuarioId == usuarioId).ToListAsync();
        }

        public void Adicionar(Movimento movimento)
        {
            _context.Movimentos.Add(movimento);
        }

        public void Atualizar(Movimento movimento)
        {
            _context.Movimentos.Update(movimento);
        }



        #endregion

        #region ContasPagar

        public async Task<ContasPagar> ObterContaPagarPorId(Guid id)
        {
            return await _context.ContasPagar.FindAsync(id);
        }

        public async Task<IEnumerable<ContasPagar>> ObterTodosContaPagar(Guid usuarioId)
        {
            return await _context.ContasPagar.AsNoTracking().Where(c => c.UsuarioId == usuarioId).ToListAsync();
        }
        
        public async Task<IEnumerable<ContasPagar>> ObterContaPagarPorCategoria(Guid categoriaId)
        {
            return await _context.ContasPagar.AsNoTracking().Where(c => c.CategoriaId == categoriaId).ToListAsync();
        }
    
        public void Adicionar(ContasPagar contasPagar)
        {
            _context.ContasPagar.Add(contasPagar);
        }

        public void Atualizar(ContasPagar contasPagar)
        {
            _context.ContasPagar.Update(contasPagar);
        }

        public void Deletar(ContasPagar contasPagar)
        {
            _context.ContasPagar.Remove(contasPagar);
        }

        public void DeletarTodos(IEnumerable<ContasPagar> contasPagar)
        {
            _context.ContasPagar.RemoveRange(contasPagar);
        }


        #endregion

        #region Parcelas


        public void Adicionar(Parcela parcela)
        {
            _context.Parcelas.Add(parcela);
        }

        public void AdicionarTodos(IEnumerable<Parcela> parcelas)
        {
            _context.Parcelas.AddRange(parcelas);
        }

        public void DeletarTodos(IEnumerable<Parcela> parcelas)
        {
            _context.Parcelas.RemoveRange(parcelas);
        }


        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
        
    }
}
