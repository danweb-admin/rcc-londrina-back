using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class SobreMap : IEntityTypeConfiguration<Sobre>
    {
        public void Configure(EntityTypeBuilder<Sobre> builder)
        {
            builder.ToTable("Sobre");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .IsRequired();


            // 🔹 Armazena a lista de Conteudo como JSON (opção mais comum no EF Core moderno)
            builder.Property(s => s.Conteudo)
                .HasColumnType("nvarchar(max)");
                

            builder.Property(s => s.EventoId)
                .IsRequired();

            // 🔹 Relacionamento 1:1 com Evento
            builder.HasOne(s => s.Evento)
                .WithOne(e => e.Sobre)
                .HasForeignKey<Sobre>(l => l.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
