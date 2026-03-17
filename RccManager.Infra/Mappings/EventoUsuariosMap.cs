using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class EventoUsuariosMap : IEntityTypeConfiguration<EventoUsuarios>
    {
        public void Configure(EntityTypeBuilder<EventoUsuarios> builder)
        {
            builder.ToTable("EventoUsuarios");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.EventoId)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired();


            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updatedAt");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("active");

            builder.HasOne(eu => eu.Evento)
               .WithMany(e => e.EventoUsuarios)
               .HasForeignKey(e => e.EventoId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(eu => eu.User)
               .WithMany(e => e.EventoUsuarios)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

