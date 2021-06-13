using System;
using System.Collections.Generic;
using System.Text;
using GoFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoFinance.Data.Mapping
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasColumnType("varchar(100)");

            builder.Property(c => c.Codigo)
                   .IsRequired()
                   .HasColumnName("Codigo")
                   .HasColumnType("int");

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasColumnName("Ativo")
                   .HasColumnType("bit");

            builder.HasOne(c => c.Usuario)
                   .WithMany(c => c.Categorias)
                   .HasForeignKey(c => c.UsuarioId);

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.ToTable("Categorias");
        }
    }
}
