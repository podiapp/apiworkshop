using ApiWorkshop.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiWorkshop.Application.Data.Mappers;

public class DrawMap : IEntityTypeConfiguration<Draw>
{
    public void Configure(EntityTypeBuilder<Draw> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Entrants)
            .WithOne(x => x.Draw);

        builder
            .Navigation(b => b.Entrants)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
