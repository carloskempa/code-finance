using CodeFinance.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFinance.Data.Mapping
{
    public class MovimentacaoMapping : IEntityTypeConfiguration<Movimentacao>
    {
        public void Configure(EntityTypeBuilder<Movimentacao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Titulo)
                   .IsRequired()
                   .HasColumnName("Titulo")
                   .HasColumnType($"varchar({Movimentacao.TITULO_LENGHT})");

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasColumnName("Descricao")
                   .HasColumnType($"varchar({Movimentacao.DESCRICAO_LENGHT})");

            builder.Property(c => c.Valor)
                   .IsRequired()
                   .HasColumnName("Valor")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Tipo)
                   .IsRequired()
                   .HasColumnName("Tipo")
                   .HasColumnType("int");

            builder.Property(c => c.DataMovimento)
                   .IsRequired()
                   .HasColumnName("DataMovimento")
                   .HasColumnType("datetime");

            builder.Property(c => c.DataCadastro)
                   .IsRequired()
                   .HasColumnName("DataCadastro")
                   .HasColumnType("datetime");

            builder.HasOne(c => c.Usuario)
                   .WithMany(c => c.Movimentacoes)
                   .HasForeignKey(c => c.UsuarioId);

            builder.HasOne(c => c.Categoria)
                   .WithMany(c => c.Movimentacoes)
                   .HasForeignKey(c => c.UsuarioId);

            builder.ToTable("Movimentacao");
        }
    }
}
