using ApiWorkshop.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWorkshop.Application.Data.Mappers;

public class PrizeDrawMap : IEntityTypeConfiguration<PrizeDraw>
{
    public void Configure(EntityTypeBuilder<PrizeDraw> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Gift)
            .WithMany(x => x.PrizeDraws)
            .HasForeignKey(x => x.GiftId);

        builder
            .Navigation(b => b.Gift)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
