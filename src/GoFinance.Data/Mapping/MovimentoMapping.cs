using GoFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoFinance.Data.Mapping
{
    public class MovimentoMapping : IEntityTypeConfiguration<Movimento>
    {
        public void Configure(EntityTypeBuilder<Movimento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeDescritivo)
                   .IsRequired()
                   .HasColumnName("NomeDescritivo")
                   .HasColumnType("varchar(250)");

            builder.Property(c => c.DtMovimento)
                   .IsRequired()
                   .HasColumnName("DtMovimento")
                   .HasColumnType("datetime");

            builder.Property(c => c.Descricao)
                   .HasColumnName("Descricao")
                   .HasColumnType("varchar(500)");

            builder.Property(c => c.Valor)
                   .IsRequired()
                   .HasColumnName("Valor")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.TipoMovimento)
                   .IsRequired()
                   .HasColumnName("TipoMovimento")
                   .HasColumnType("int");

            builder.HasOne(c => c. ContasPagar )
                   .WithMany(c => c.Movimentos)
                   .HasForeignKey(c => c.ContaPagarId);

            builder.HasOne(c => c.ContaFinanceira)
                   .WithMany(c => c.Movimentos)
                   .HasForeignKey(c => c.ContaFinanceiraId);

            builder.HasOne(c => c.Usuario)
                   .WithMany(c => c.Movimentacoes)
                   .HasForeignKey(c => c.UsuarioId);

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.ToTable("Smtp");
        }
    }
}
