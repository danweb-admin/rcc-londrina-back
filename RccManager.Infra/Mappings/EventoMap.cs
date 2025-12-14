using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.ToTable("Eventos");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.BannerImagem)
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.DataInicio)
                .IsRequired();

            builder.Property(e => e.DataFim)
                .IsRequired();

            builder.Property(e => e.OrganizadorNome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.OrganizadorEmail)
                .HasMaxLength(150);

            builder.Property(e => e.OrganizadorContato)
                .HasMaxLength(30);

            builder.Property(e => e.Status)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.ExibirPregadores)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.ExibirProgramacao)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.ExibirInformacoesAdicionais)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updatedAt");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("active");

            builder.Property(e => e.HabilitarPix)
                .HasColumnType("bit");

            builder.Property(e => e.HabilitarCartao)
                .HasColumnType("bit");

            builder.Property(e => e.QtdParcelas)
                .HasColumnType("int");

            // 🔹 Relacionamentos 1:1
            

            builder.HasOne(e => e.Local)
                .WithOne(l => l.Evento)
                .HasForeignKey<Local>(l => l.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Sobre)
                .WithOne(s => s.Evento)
                .HasForeignKey<Sobre>(l => l.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.InformacoesAdicionais)
                .WithOne(i => i.Evento)
                .HasForeignKey<InformacoesAdicionais>(l => l.EventoId)
                .OnDelete(DeleteBehavior.Cascade);


            // 🔹 Relacionamentos 1:N




            builder.HasMany(e => e.Participacoes)
                .WithOne(p => p.Evento)
                .HasForeignKey(p => p.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.LotesInscricoes)
                .WithOne(l => l.Evento)
                .HasForeignKey(l => l.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Programacao)
                .WithOne(p => p.Evento)
                .HasForeignKey(p => p.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Inscricoes)
                .WithOne(i => i.Evento)
                .HasForeignKey(i => i.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
