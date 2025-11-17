using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class ParticipacaoMap : IEntityTypeConfiguration<Participacao>
    {
        public void Configure(EntityTypeBuilder<Participacao> builder)
        {
            builder.ToTable("Participacoes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Foto)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(p => p.Descricao)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(p => p.EventoId)
                .IsRequired();

            // 🔹 Relacionamento muitos-para-um (várias Participacoes para um Evento)
            builder.HasOne(p => p.Evento)
                .WithMany(e => e.Participacoes)
                .HasForeignKey(p => p.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
