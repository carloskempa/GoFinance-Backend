using GoFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoFinance.Data.Mapping
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                  .IsRequired()
                  .HasColumnName("Nome")
                  .HasColumnType("varchar(100)");

            builder.Property(c => c.UrlSite)
                  .HasColumnName("UrlSite")
                  .HasColumnType("varchar(250)");

            builder.OwnsOne(c => c.CnpjCpf, cm =>
            {
                cm.Property(c => c.NumeroDocumento)
                  .HasColumnName("NumeroDocumento")
                  .HasColumnType("varchar(14)");
            });

            builder.Property(c => c.Descricao)
                  .HasColumnName("Descricao")
                  .HasColumnType("varchar(500)");

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasColumnName("Ativo")
                   .HasColumnType("bit");

            builder.HasOne(c => c.Usuario)
                   .WithMany(c => c.Fornecedores)
                   .HasForeignKey(c => c.UsuarioId);

            builder.Property(c => c.DtCadastro)
                  .IsRequired()
                  .HasColumnName("DtCadastro")
                  .HasColumnType("datetime");

            builder.ToTable("Fornecedores");
        }
    }
}
