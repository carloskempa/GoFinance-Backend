using GoFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoFinance.Data.Mapping
{
    public class SmtpMapping : IEntityTypeConfiguration<Smtp>
    {
        public void Configure(EntityTypeBuilder<Smtp> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.Email, cm =>
            {
                cm.Property(c => c.Endereco)
                  .HasColumnName("Email")
                  .HasColumnType("varchar(200)");
            });

            builder.Property(c => c.Mascara)
                  .IsRequired()
                  .HasColumnName("Nome")
                  .HasColumnType("varchar(100)");

            builder.Property(c => c.Host)
                   .IsRequired()
                   .HasColumnName("Host")
                   .HasColumnType("varchar(150)");

            builder.Property(c => c.Usuario)
                   .IsRequired()
                   .HasColumnName("Usuario")
                   .HasColumnType("varchar(150)");

            builder.Property(c => c.Senha)
                   .IsRequired()
                   .HasColumnName("Senha")
                   .HasColumnType("varchar(100)");

            builder.Property(c => c.Porta)
                   .IsRequired()
                   .HasColumnName("Porta")
                   .HasColumnType("int");

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasColumnName("Ativo")
                   .HasColumnType("bit");

            builder.Property(c => c.SSL)
                   .IsRequired()
                   .HasColumnName("SSL")
                   .HasColumnType("bit");

            builder.Property(c => c.DtCadastro)
                 .IsRequired()
                 .HasColumnName("DtCadastro")
                 .HasColumnType("datetime");

            builder.ToTable("Smtp");
        }
    }
}
