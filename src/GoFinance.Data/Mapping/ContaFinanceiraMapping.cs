using System;
using System.Collections.Generic;
using System.Text;
using GoFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoFinance.Data.Mapping
{
    public class ContaFinanceiraMapping : IEntityTypeConfiguration<ContaFinanceira>
    {
        public void Configure(EntityTypeBuilder<ContaFinanceira> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasColumnType("varchar(100)");

            builder.Property(c => c.Banco)
                   .IsRequired()
                   .HasColumnName("Banco")
                   .HasColumnType("varchar(200)");

            builder.Property(c => c.Saldo)
                   .IsRequired()
                   .HasColumnName("Saldo")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.Property(c => c.Descricao)
                   .HasColumnName("Descricao")
                   .HasColumnType("varchar(500)");

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasColumnName("Ativo")
                   .HasColumnType("bit");

            builder.HasOne(c => c.Usuario)
                   .WithMany(c => c.ContaFinanceiras)
                   .HasForeignKey(c => c.UsuarioId);

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.ToTable("ContaFinanceira");
        }
    }
}
