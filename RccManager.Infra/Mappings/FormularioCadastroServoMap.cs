using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class FormularioCadastroServoMap : IEntityTypeConfiguration<FormularioCadastroServo>
    {
        public void Configure(EntityTypeBuilder<FormularioCadastroServo> builder)
        {
            builder.ToTable("FormularioCadastroServo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.GrupoOracaoId)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("active");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updatedAt");

            builder.Property(x => x.Token);

            // Relacionamento com a entidade grupo oracao
            builder.HasOne(x => x.GrupoOracao)
                .WithMany()
                .HasForeignKey(x => x.GrupoOracaoId);

            
        }
    }
}

