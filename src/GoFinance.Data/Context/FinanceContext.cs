using System;
using System.Linq;
using System.Threading.Tasks;
using GoFinance.Data.Mapping;
using GoFinance.Domain.Core.Data;
using GoFinance.Domain.Core.Messages;
using GoFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoFinance.Data.Context
{
    public class FinanceContext : DbContext, IUnitOfWork
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<ContaFinanceira> ContasFinanceiras { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<Smtp> Smtp { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<ContasPagar> ContasPagar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfiguration(new CategoriaMapping());
            modelBuilder.ApplyConfiguration(new ContaFinanceiraMapping());
            modelBuilder.ApplyConfiguration(new ContasPagarMapping());
            modelBuilder.ApplyConfiguration(new FornecedorMapping());
            modelBuilder.ApplyConfiguration(new MovimentoMapping());
            modelBuilder.ApplyConfiguration(new ParcelaMapping());
            modelBuilder.ApplyConfiguration(new SmtpMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DtCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DtCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DtCadastro").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
