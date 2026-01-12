using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class UsuariosCheckinMap : IEntityTypeConfiguration<UsuariosCheckin>
    {
        public void Configure(EntityTypeBuilder<UsuariosCheckin> builder)
        {
            builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Nome)
            .HasColumnType("varchar(150)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnType("varchar(150)")
            .HasMaxLength(4);

        builder.Property(c => c.Senha)
            .HasColumnType("varchar(150)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.EventoId);

        builder.Property(c => c.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .HasColumnType("datetime");


        builder.Property(c => c.Active)
            .HasColumnType("bit")
            .IsRequired();

        // 🔹 Relacionamento 1:N com Evento
            builder.HasOne(s => s.Evento)
                .WithMany(e => e.UsuariosCheckin);
        }
    }
}

