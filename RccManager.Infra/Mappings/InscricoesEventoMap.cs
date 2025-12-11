using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
	public class InscricoesEventoMap : IEntityTypeConfiguration<PagamentoAsaas>
    {

        public void Configure(EntityTypeBuilder<PagamentoAsaas> builder)
        {
            builder.ToTable("InscricoesEvento");
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
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(200);

            builder.Property(x => x.AmountPaid)
                .HasColumnName("amount_paid")
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Value)
                .HasColumnName("value")
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasMaxLength(20);

            builder.Property(x => x.Registered)
                .HasColumnName("registered")
                .HasMaxLength(1);

            builder.Property(x => x.GrupoOracaoId);

            builder.Property(x => x.EventId)
                .IsRequired();

            builder.HasOne(x => x.Evento)
                .WithMany()
                .HasForeignKey(x => x.EventId);

        }
    }
}

