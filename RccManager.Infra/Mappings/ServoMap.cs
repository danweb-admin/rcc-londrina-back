using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class ServoMap : IEntityTypeConfiguration<Servo>
    {
        public void Configure(EntityTypeBuilder<Servo> builder)
        {
            builder.ToTable("Servos");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("active");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updatedAt");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(100);

            builder.Property(x => x.Birthday)
                .IsRequired()
                .HasColumnName("birthday");

            builder.Property(x => x.CellPhone)
                .IsRequired()
                .HasColumnName("cellphone")
                .HasMaxLength(15);

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnName("cpf")
                .HasMaxLength(14);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(80);

            builder.Property(x => x.MainMinistry)
                .IsRequired()
                .HasColumnName("main_ministry")
                .HasMaxLength(30);

            builder.Property(x => x.SecondaryMinistry)
                .HasColumnName("secondary_ministry")
                .HasMaxLength(30);

            builder.Property(x => x.GrupoOracaoId)
                .IsRequired()
                .HasColumnName("grupoOracaoId");

            builder.HasOne(x => x.GrupoOracao)
                .WithMany(x => x.Servos)
                .HasForeignKey(x => x.GrupoOracaoId);



        }
    }
}

