using System;
using System.Collections.Generic;
using System.Text;
using GoFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoFinance.Data.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).IsRequired();

            

            // 1 : N => Usuario : Categorias
            builder.HasMany(c => c.Categorias)
                   .WithOne(c => c.Usuario)
                   .HasForeignKey(c => c.Id);

            // 1 : N => Usuario : Fornecedores
            builder.HasMany(c => c.Fornecedores)
                   .WithOne(c => c.Usuario)
                   .HasForeignKey(c => c.Id);

            // 1 : N => Usuario : ContaFinanceira
            builder.HasMany(c => c.ContaFinanceiras)
                   .WithOne(c => c.Usuario)
                   .HasForeignKey(c => c.Id);

        }
    }
}
