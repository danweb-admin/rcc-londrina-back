using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using RccManager.Domain.Entities;

namespace RccManager.Infra.Mappings
{
    public class EventoCamposMap : IEntityTypeConfiguration<EventoCampos>
    {
        public void Configure(EntityTypeBuilder<EventoCampos> builder)
        {
            builder.ToTable("EventoCampos");

           

            builder
            .Property(e => e.Opcoes)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v)
            );
        }
    }
}

