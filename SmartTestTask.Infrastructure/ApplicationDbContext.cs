using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SmartTestTask.Domain.Entities;
using SmartTestTask.Domain.Entities.Abstract;

namespace SmartTestTask.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<IndustrialPremise> IndustrialPremises { get; set; }
    public DbSet<TechnicalEquipmentType> TechnicalEquipmentTypes { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        if (!Database.IsInMemory())
        {
            Database.Migrate();
        }
    }

    public override int SaveChanges()
    {
        AddInfoBeforeUpdate();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        AddInfoBeforeUpdate();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddInfoBeforeUpdate()
    {
        var entries = ChangeTracker.Entries()
            .Where(x => x.Entity is Entity && x.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((Entity)entry.Entity).CreatedAt = DateTime.UtcNow;
            }

            ((Entity)entry.Entity).UpdatedAt = DateTime.UtcNow;
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext))!);
    }
}