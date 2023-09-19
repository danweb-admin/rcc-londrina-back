using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class ParoquiaCapelaMap : IEntityTypeConfiguration<ParoquiaCapela>
    {
        public void Configure(EntityTypeBuilder<ParoquiaCapela> builder)
        {
            builder.ToTable("ParoquiasCapelas");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasColumnName("address")
                .HasMaxLength(100);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(100);

            builder.Property(x => x.Neighborhood)
                .IsRequired()
                .HasColumnName("neighborhood")
                .HasMaxLength(50);

            builder.Property(x => x.DecanatoId)
                .IsRequired()
                .HasColumnName("decanatoId");

            builder.Property(x => x.City)
                .IsRequired()
                .HasColumnName("city")
                .HasMaxLength(50);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("active");

            builder.Property(x => x.ZipCode)
                .HasColumnName("zipCode");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updatedAt");

            builder.HasOne(x => x.DecanatoSetor)
                .WithMany()
                .HasForeignKey(x => x.DecanatoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
