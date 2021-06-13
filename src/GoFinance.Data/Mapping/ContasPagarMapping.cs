using GoFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoFinance.Data.Mapping
{
    public class ContasPagarMapping : IEntityTypeConfiguration<ContasPagar>
    {
        public void Configure(EntityTypeBuilder<ContasPagar> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasColumnType("varchar(100)");

            builder.Property(c => c.NumeroParcelas)
                   .IsRequired()
                   .HasColumnName("NumeroParcelas")
                   .HasColumnType("int");

            builder.Property(c => c.ValorTotal)
                   .IsRequired()
                   .HasColumnName("ValorTotal")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Observacoes)
                   .HasColumnName("Observacoes")
                   .HasColumnType("varchar(500)");

            builder.HasOne(c => c.Usuario)
                   .WithMany(c => c.ContasPagar)
                   .HasForeignKey(c => c.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Categoria)
                   .WithMany(c => c.ContasPagar)
                   .HasForeignKey(c => c.CategoriaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Fornecedor)
                   .WithMany(c => c.ContasPagar)
                   .HasForeignKey(c => c. FornecedorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.ToTable("ContasPagar");
        }
    }
}
