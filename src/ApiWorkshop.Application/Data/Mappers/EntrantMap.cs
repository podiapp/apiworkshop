using ApiWorkshop.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiWorkshop.Application.Data.Mappers;

public class EntrantMap : IEntityTypeConfiguration<Entrant>
{
    public void Configure(EntityTypeBuilder<Entrant> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Draw)
            .WithMany(x => x.Entrants);

        builder
            .Navigation(b => b.Draw)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
