using CodeFinance.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFinance.Data.Mapping
{
    public class MetaMapping : IEntityTypeConfiguration<Meta>
    {
        public void Configure(EntityTypeBuilder<Meta> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasColumnType($"varchar({Meta.NOME_LENGHT})");

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasColumnName("Descricao")
                   .HasColumnType($"varchar({Meta.DESCRICAO_LENGHT})");

            builder.Property(c => c.SaldoAtualMeta)
                   .IsRequired()
                   .HasColumnName("SaldoAtualMeta")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.ValorMeta)
                   .IsRequired()
                   .HasColumnName("ValorMeta")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.DataInicio)
                   .IsRequired()
                   .HasColumnName("DataInicio")
                   .HasColumnType("datetime");

            builder.Property(c => c.DataFinal)
                   .IsRequired()
                   .HasColumnName("DataFinal")
                   .HasColumnType("datetime");

            builder.Property(c => c.DataCadastro)
                   .IsRequired()
                   .HasColumnName("DataCadastro")
                   .HasColumnType("datetime");

            builder.Property(c => c.UrlImagemCapa)
                   .HasColumnName("UrlImageCapa")
                   .HasColumnType("varchar(1000)");

            builder.HasOne(c => c.Usuario)
                   .WithMany(u => u.Metas)
                   .HasForeignKey(c => c.UsuarioId);

            builder.ToTable("Meta");
        }
    }
}
