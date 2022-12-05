using ApiWorkshop.Application.Data.Mappers;
using ApiWorkshop.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiWorkshop.Application.Data.Context;

public class ApplicationContext : DbContext
{
    public DbSet<Gift>? Gifts { get; set; }
    public DbSet<PrizeDraw>? PrizeDraws { get; set; }
    public DbSet<Draw>? Draws { get; set; }
    public DbSet<Entrant>? Entrants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("workshop");
        modelBuilder.Entity<Gift>(new GiftMap().Configure);
        modelBuilder.Entity<PrizeDraw>(new PrizeDrawMap().Configure);
        modelBuilder.Entity<Draw>(new DrawMap().Configure);
        modelBuilder.Entity<Entrant>(new EntrantMap().Configure);
    }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
}