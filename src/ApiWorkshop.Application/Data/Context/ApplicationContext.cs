using ApiWorkshop.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiWorkshop.Application.Data.Context;

public class ApplicationContext : DbContext
{
    public DbSet<Gift>? Gifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("workshop");
        //modelBuilder.Entity<Invoice>(new InvoiceMap().Configure);
    }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
}