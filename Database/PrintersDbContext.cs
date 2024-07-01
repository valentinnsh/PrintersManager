using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class PrintersDbContext : DbContext
{
    public PrintersDbContext(DbContextOptions<PrintersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        OnCommonModelCreating(builder);
    }

    protected void OnCommonModelCreating(ModelBuilder modelBuilder)
    {
        var printers = modelBuilder.Entity<PrinterEntity>().ToTable("printers");
    }
    
    public IQueryable<PrinterEntity> Printers => Set<PrinterEntity>();
}