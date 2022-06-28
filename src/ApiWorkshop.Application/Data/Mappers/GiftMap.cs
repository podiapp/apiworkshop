using ApiWorkshop.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiWorkshop.Application.Data.Mappers
{
    public class GiftMap : IEntityTypeConfiguration<Gift>
    {
        public void Configure(EntityTypeBuilder<Gift> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.PrizeDraws)
                .WithOne(x => x.Gift);

            builder
                .Navigation(b => b.PrizeDraws)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
