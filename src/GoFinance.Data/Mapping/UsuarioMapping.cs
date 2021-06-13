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

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasColumnType("varchar(100)");
            
            builder.Property(c => c.Login)
                   .IsRequired()
                   .HasColumnName("Login")
                   .HasColumnType("varchar(50)");

            builder.OwnsOne(c => c.Email, cm =>
            {
                cm.Property(c => c.Endereco)
                  .HasColumnName("Email")
                  .HasColumnType("varchar(200)");
            });

            builder.Property(c => c.Administrador)
                   .IsRequired()
                   .HasColumnName("Administrador")
                   .HasColumnType("bit");

            builder.Property(c => c.Senha)
                   .IsRequired()
                   .HasColumnName("Senha")
                   .HasColumnType("varbinary(max)");

            builder.Property(c => c.TokenAlteracaoSenha)
                   .HasColumnName("TokenAlteracaoSenha")
                   .HasColumnType("varchar(100)");

            builder.Property(c => c.DataExpiracaoToken)
                   .HasColumnName("DataExpiracaoToken")
                   .HasColumnType("datetime");

            builder.Property(c => c.RefleshToken)
                   .HasColumnName("RefleshToken")
                   .HasColumnType("varchar(100)");

            builder.Property(c => c.DataExpiracaoRefleshToken)
                   .HasColumnName("DataExpiracaoRefleshToken")
                   .HasColumnType("datetime");

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasColumnName("Ativo")
                   .HasColumnType("bit");

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.ToTable("Usuarios");
        }
    }
}
