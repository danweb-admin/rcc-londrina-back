using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class LoteInscricaoMap : IEntityTypeConfiguration<LoteInscricao>
    {
        public void Configure(EntityTypeBuilder<LoteInscricao> builder)
        {
            builder.ToTable("LotesInscricao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.EventoId)
                .HasColumnName("EventoId")
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.DataInicio)
                .HasColumnName("DataInicio")
                .IsRequired();

            builder.Property(x => x.DataFim)
                .HasColumnName("DataFim")
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasColumnName("Valor")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            // 🔗 Relacionamento com Evento
            builder.HasOne(x => x.Evento)
                .WithMany(x => x.LotesInscricoes)
                .HasForeignKey(x => x.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
