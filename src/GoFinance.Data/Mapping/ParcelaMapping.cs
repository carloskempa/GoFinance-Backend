using GoFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoFinance.Data.Mapping
{
    public class ParcelaMapping : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeDescritivo)
                   .IsRequired()
                   .HasColumnName("NomeDescritivo")
                   .HasColumnType("varchar(250)");

            builder.Property(c => c.NumeroParcela)
                   .IsRequired()
                   .HasColumnName("NumeroParcela")
                   .HasColumnType("int");

            builder.Property(c => c.Valor)
                   .IsRequired()
                   .HasColumnName("Valor")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Desconto)
                   .HasColumnName("Desconto")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Multa)
                   .HasColumnName("Multa")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Juros)
                   .HasColumnName("Juros")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.StatusParcela)
                   .HasColumnName("StatusParcela")
                   .HasColumnType("int");

            builder.Property(c => c.DtCompetencia)
                   .IsRequired()
                   .HasColumnName("DtCompetencia")
                   .HasColumnType("datetime");

            builder.Property(c => c.DtPagamento)
                   .HasColumnName("DtPagamento")
                   .HasColumnType("datetime");

            builder.Property(c => c.DtVencimento)
                  .IsRequired()
                  .HasColumnName("DtVencimento")
                  .HasColumnType("datetime");

            builder.HasOne(c => c.ContaFinanceira)
                   .WithMany(c => c.Parcelas)
                   .HasForeignKey(c => c.ContaFinanceiraId);

            builder.HasOne(c => c.ContaPagar)
                   .WithMany(c => c.Parcelas)
                   .HasForeignKey(c => c.ContaPagarId);

            builder.Property(c => c.DtCadastro)
                  .IsRequired()
                  .HasColumnName("DtCadastro")
                  .HasColumnType("datetime");

            builder.ToTable("Parcelas");
        }
    }
}
