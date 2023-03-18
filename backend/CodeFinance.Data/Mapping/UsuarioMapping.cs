using CodeFinance.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFinance.Data.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasColumnType($"varchar({Usuario.NOME_LENGHT})");

            builder.Property(c => c.Sobrenome)
                   .IsRequired()
                   .HasColumnName("Sobrenome")
                   .HasColumnType($"varchar({Usuario.SOBRENOME_LENGHT})");

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasColumnName("Email")
                   .HasColumnType($"varchar({Usuario.EMAIL_LENGHT})");

            builder.Property(c => c.Senha)
                   .IsRequired()
                   .HasColumnName("Senha")
                   .HasColumnType("varchar(max)");

            builder.Property(c => c.Administrador)
                   .HasColumnName("Administrador")
                   .HasColumnType("bit");

            builder.Property(c => c.DataCadastro)
                   .IsRequired()
                   .HasColumnName("DataCadastro")
                   .HasColumnType("datetime");

            builder.Property(c => c.Status)
                   .IsRequired()
                   .HasColumnName("Status")
                   .HasColumnType("int");

            builder.HasOne(u => u.UsuarioPai)
                   .WithMany(u => u.UsuarioFilhos)
                   .HasForeignKey(u => u.UsuarioPaiId);

            builder.HasOne(u => u.Saldo)
                   .WithOne(s => s.Usuario)
                   .HasForeignKey<Saldo>(s => s.UsuarioId);

            builder.ToTable("Usuario");
        }
    }
}
