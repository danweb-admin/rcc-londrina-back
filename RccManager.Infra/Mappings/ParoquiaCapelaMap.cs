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

            builder.Property(x => x.Endereco)
                .IsRequired()
                .HasColumnName("endereco")
                .HasMaxLength(100);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(100);

            builder.Property(x => x.Bairro)
                .IsRequired()
                .HasColumnName("bairro")
                .HasMaxLength(50);

            builder.Property(x => x.DecanatoId)
                .IsRequired()
                .HasColumnName("decanatoId");

            builder.Property(x => x.Cidade)
                .IsRequired()
                .HasColumnName("cidade")
                .HasMaxLength(50);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("createdAt");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("ativo");

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updatedAt");

            builder.HasOne(x => x.DecanatoSetor)
                .WithMany()
                .HasForeignKey(x => x.DecanatoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
