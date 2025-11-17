using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class InscricaoMap : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder.ToTable("Inscricoes");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .IsRequired();

            builder.Property(l => l.Active)
                .HasColumnType("bit")
                .IsRequired(true);

            builder.Property(l => l.Cpf)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(l => l.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(l => l.DecanatoId);

            builder.Property(l => l.Decanato)
                .HasColumnType("varchar(100)");


            builder.Property(l => l.EventoId)
                .IsRequired();

            builder.Property(l => l.GrupoOracaoId);

            builder.Property(l => l.GrupoOracao)
                .HasColumnType("varchar(200)");


            builder.Property(l => l.Email)
                .HasColumnType("varchar(50)");

            builder.Property(l => l.Nome)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(l => l.Telefone)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.Property(l => l.CodigoInscricao)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(l => l.DataPagamento)
                .HasColumnType("datetime");

            builder.Property(l => l.ValorInscricao)
                .HasColumnType("decimal(10,2)");

            builder.Property(l => l.Status)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(l => l.TipoPagamento)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(l => l.LinkQrCodePNG)
                .HasColumnType("varchar(200)");

            builder.Property(l => l.LinkQrCodeBase64)
                .HasColumnType("varchar(200)");

            builder.Property(l => l.QRCodeCopiaCola)
                .HasColumnType("varchar(200)");
        }
    }
}

