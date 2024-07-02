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
        var employees = modelBuilder.Entity<EmployeeEntity>().ToTable("employees");
        var branches = modelBuilder.Entity<BranchEntity>().ToTable("branches");
        var installations = modelBuilder.Entity<InstallationEntity>().ToTable("installations");

        printers.HasMany(printer => printer.Installations)
            .WithOne(installation => installation.Printer)
            .HasForeignKey(installation => installation.PrinterId)
            .HasPrincipalKey(printer => printer.Id);
        
        branches.HasMany(branch => branch.Installations)
            .WithOne(installation => installation.Branch)
            .HasForeignKey(installation => installation.BranchId)
            .HasPrincipalKey(branch => branch.Id);
        
        branches.HasMany(branch => branch.Employees)
            .WithOne(employee => employee.Branch)
            .HasForeignKey(employee => employee.BranchId)
            .HasPrincipalKey(branch => branch.Id);
    }
    
    public IQueryable<PrinterEntity> Printers => Set<PrinterEntity>();
    public IQueryable<EmployeeEntity> Employees => Set<EmployeeEntity>();
    public IQueryable<BranchEntity> Branchs => Set<BranchEntity>();
    public IQueryable<InstallationEntity> Installations => Set<InstallationEntity>();

}