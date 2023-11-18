using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class FormacoesServoMap : IEntityTypeConfiguration<FormacoesServo>
    {
        public void Configure(EntityTypeBuilder<FormacoesServo> builder)
        {
            builder.ToTable("FormacoesServos");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ServoId)
                .HasColumnName("servoId")
                .IsRequired();

            builder.Property(x => x.FormacaoId)
                .HasColumnName("formacaoId")
                .IsRequired();

            builder.Property(x => x.UsuarioId)
                .HasColumnName("usuarioId")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("active");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updatedAt");

            builder.Property(x => x.CertificateDate)
                .HasColumnName("certificateDate");

            // Relacionamento com a entidade Servo
            builder.HasOne(x => x.Servo)
                .WithMany()
                .HasForeignKey(x => x.ServoId);

            builder.HasOne(x => x.Formacao)
                .WithMany()
                .HasForeignKey(x => x.FormacaoId);

            builder.HasOne(x => x.Usuario)
                .WithMany()
                .HasForeignKey(x => x.UsuarioId);
        }
    }
}

