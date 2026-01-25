using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class TransferenciaServoMap : IEntityTypeConfiguration<TransferenciaServo>
    {
        

        public void Configure(EntityTypeBuilder<TransferenciaServo> builder)
        {
            builder.ToTable("TransferenciaServos");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("active");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updatedAt");

            builder.Property(x => x.ServoId)
                .IsRequired();

            builder.Property(x => x.GrupoOracaoAntigoId)
                .IsRequired();

            builder.Property(x => x.GrupoOracaoId)
                .IsRequired();

            builder.Property(x => x.Efetuado)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Solicitado)
                .IsRequired()
                .HasMaxLength(100);
            

            builder.HasOne(x => x.GrupoOracao)
                .WithMany()
                .HasForeignKey(x => x.GrupoOracaoId);

            builder.HasOne(x => x.GrupoOracaoAntigo)
                .WithMany()
                .HasForeignKey(x => x.GrupoOracaoAntigoId);

            builder.HasOne(x => x.Servo)
                .WithMany()
                .HasForeignKey(x => x.ServoId);
        }
    }
}

