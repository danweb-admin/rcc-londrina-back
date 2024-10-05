using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class GrupoOracaoMap : IEntityTypeConfiguration<GrupoOracao>
    {
        public void Configure(EntityTypeBuilder<GrupoOracao> builder)
        {
            builder.ToTable("GrupoOracoes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(80);

            builder.Property(x => x.ParoquiaId)
                .IsRequired()
                .HasColumnName("paroquiaId");

            builder.Property(x => x.Type)
                .IsRequired()
                .HasColumnName("type")
                .HasMaxLength(15);

            builder.Property(x => x.DayOfWeek)
                .HasColumnName("dayOfWeek")
                .HasMaxLength(10);

            builder.Property(x => x.Local)
                .HasColumnName("local")
                .HasMaxLength(50);

            builder.Property(x => x.Time)
                .HasColumnName("time");

            builder.Property(x => x.FoundationDate)
                .HasColumnName("foundationDate");

            builder.Property(x => x.Address)
                .HasColumnName("address")
                .HasMaxLength(100);

            builder.Property(x => x.Neighborhood)
                .HasColumnName("neighborhood")
                .HasMaxLength(50);

            builder.Property(x => x.ZipCode)
                .HasColumnName("zipCode")
                .HasMaxLength(9);

            builder.Property(x => x.City)
                .HasColumnName("city")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(80);

            builder.Property(x => x.Site)
                .HasColumnName("site")
                .HasMaxLength(50);

            builder.Property(x => x.Telephone)
                .HasColumnName("telephone")
                .HasMaxLength(15);

            builder.Property(x => x.NumberOfParticipants)
                .HasColumnName("numberOfParticipants");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updatedAt");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("active");

            builder.Property(x => x.FormsUrl)
                .HasColumnName("formsUrl")
                .HasMaxLength(300);

            builder.Property(x => x.CsvUrl)
                .HasColumnName("csvUrl")
                .HasMaxLength(300);

            // Relacionamento com a entidade ParoquiaCapela
            builder.HasOne(x => x.ParoquiaCapela)
                .WithMany()
                .HasForeignKey(x => x.ParoquiaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
