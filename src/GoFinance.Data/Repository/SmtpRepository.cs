using GoFinance.Data.Context;
using GoFinance.Domain.Core.Data;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoFinance.Data.Repository
{
    public class SmtpRepository : ISmtpRepository
    {
        private readonly FinanceContext _context;

        public SmtpRepository(FinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Smtp> ObterAtivo()
        {
            return await _context.Smtp.FirstOrDefaultAsync(c => c.Ativo == true);
        }

        public async Task<Smtp> ObterPorId(Guid id)
        {
            return await _context.Smtp.FindAsync(id);
        }

        public async Task<IEnumerable<Smtp>> ObterTodos()
        {
            return await _context.Smtp.ToListAsync();
        }

        public void Adicionar(Smtp smtp)
        {
            _context.Smtp.Add(smtp);
        }

        public void Atualizar(Smtp smtp)
        {
            _context.Smtp.Update(smtp);
        }

        public void Deletar(Smtp smtp)
        {
            _context.Smtp.Remove(smtp);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
       
    }
}
