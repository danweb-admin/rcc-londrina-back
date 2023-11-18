using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings;

public class FormacaoMap : IEntityTypeConfiguration<Formacao>
{
    public void Configure(EntityTypeBuilder<Formacao> builder)
    {
        builder.ToTable("Formacoes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(100);

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

