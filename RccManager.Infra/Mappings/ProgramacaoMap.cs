using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class ProgramacaoMap : IEntityTypeConfiguration<Programacao>
    {
        public void Configure(EntityTypeBuilder<Programacao> builder)
        {
            builder.ToTable("Programacao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Dia)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(p => p.EventoId)
                .IsRequired();

            // 🔹 Relacionamento muitos-para-um (vários Programacao para um Evento)
            builder.HasOne(p => p.Evento)
                .WithMany(e => e.Programacao)
                .HasForeignKey(p => p.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
