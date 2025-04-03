using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Name)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NickName)
            .HasColumnType("varchar(4)")
            .HasMaxLength(4);

        builder.Property(c => c.Password)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Role)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(c => c.Email)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .HasColumnType("datetime");


        builder.Property(c => c.Active)
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(c => c.GrupoOracaoId);

        builder.Property(c => c.DecanatoSetorId);

        builder.Property(c => c.ConfirmationDate)
            .HasColumnType("datetime");

        builder.Property(c => c.EmailConfirmed)
            .HasColumnType("bit");
    }
}

