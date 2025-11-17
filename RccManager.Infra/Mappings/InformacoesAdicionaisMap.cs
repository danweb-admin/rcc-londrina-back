using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;
using System.Text.Json;

namespace RccManager.Infra.Mappings
{
    public class InformacoesAdicionaisMap : IEntityTypeConfiguration<InformacoesAdicionais>
    {
        public void Configure(EntityTypeBuilder<InformacoesAdicionais> builder)
        {
            builder.ToTable("InformacoesAdicionais");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.EventoId)
                .HasColumnName("EventoId")
                .IsRequired();

            // 🔹 Armazena a lista de strings (Texto) como JSON
            builder.Property(x => x.Texto)
                .HasColumnName("Texto")
                .HasColumnType("nvarchar(max)");

            // 🔗 Relacionamento com Evento
            builder.HasOne(x => x.Evento)
                .WithOne(x => x.InformacoesAdicionais)
                .HasForeignKey<InformacoesAdicionais>(l => l.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
