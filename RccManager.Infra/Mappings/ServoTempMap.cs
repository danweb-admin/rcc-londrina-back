using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
	public class ServoTempMap : IEntityTypeConfiguration<ServoTemp>
    {
        public void Configure(EntityTypeBuilder<ServoTemp> builder)
        {
            builder.ToTable("ServosTemp");
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
                .HasMaxLength(200);

            builder.Property(x => x.Birthday)
                .IsRequired()
                .HasColumnName("birthday");

            builder.Property(x => x.CellPhone)
                .IsRequired()
                .HasColumnName("cellphone")
                .HasMaxLength(100);

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnName("cpf")
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(200);

            builder.Property(x => x.MainMinistry)
                .IsRequired()
                .HasColumnName("main_ministry")
                .HasMaxLength(30);

            builder.Property(x => x.SecondaryMinistry)
                .HasColumnName("secondary_ministry")
                .HasMaxLength(30);

            builder.Property(x => x.Checked)
                .HasColumnName("checked");

            builder.Property(x => x.GrupoOracaoId)
                .IsRequired()
                .HasColumnName("grupoOracaoId");

            builder.HasOne(x => x.GrupoOracao)
                .WithMany(x => x.ServosTemp)
                .HasForeignKey(x => x.GrupoOracaoId);



        }
    }
}

