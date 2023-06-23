using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings;

public class DecanatoSetorMap : IEntityTypeConfiguration<DecanatoSetor>
{
    public void Configure(EntityTypeBuilder<DecanatoSetor> builder)
    {
        builder.ToTable("Decanatos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(50);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("createdAt");

        builder.Property(x => x.Active)
            .IsRequired()
            .HasColumnName("active");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updatedAt");



    }
}

