using CodeFinance.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFinance.Data.Mapping
{
    public class SaldoMapping : IEntityTypeConfiguration<Saldo>
    {
        public void Configure(EntityTypeBuilder<Saldo> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Ignore(c => c.DataCadastro);
            builder.Ignore(c => c.Usuario);

            builder.Property(c => c.SaldoAtual)
                   .IsRequired()
                   .HasColumnName("SaldoAtual")
                   .HasColumnType("decimal(18,2)");

            builder.ToTable("Saldo");
        }
    }
}
