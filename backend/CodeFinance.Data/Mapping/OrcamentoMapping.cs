using CodeFinance.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFinance.Data.Mapping
{
    public class OrcamentoMapping : IEntityTypeConfiguration<Orcamento>
    {
        public void Configure(EntityTypeBuilder<Orcamento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasColumnName("Descricao")
                   .HasColumnType($"varchar({Orcamento.DESCRICAO_LENGHT})");

            builder.Property(c => c.ValorOrcamento)
                   .IsRequired()
                   .HasColumnName("ValorOrcamento")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.DataInicio)
                   .IsRequired()
                   .HasColumnName("DataInicio")
                   .HasColumnType("datetime");

            builder.Property(c => c.DataFim)
                   .IsRequired()
                   .HasColumnName("DataFim")
                   .HasColumnType("datetime");

            builder.Property(c => c.DataCadastro)
                   .IsRequired()
                   .HasColumnName("DataCadastro")
                   .HasColumnType("datetime");

            builder.HasOne(c => c.Usuario)
                   .WithMany(u => u.Orcamentos)
                   .HasForeignKey(c => c.UsuarioId);

            builder.HasOne(c => c.Categoria)
                   .WithMany(x => x.Orcamentos)
                   .HasForeignKey(c => c.CategoriaId);

            builder.ToTable("Orcamento");
        }
    }
}
