using Microsoft.EntityFrameworkCore;
using Tatweer.Core.Common;
using Tatweer.Core.Entities;
using System.Reflection;

namespace Tatweer.Insrastructure.Data;

public class TatweerContext : DbContext
{
    public TatweerContext(DbContextOptions<TatweerContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public void BeginTransaction()
    {
        if (Database.CurrentTransaction == null)
            Database.BeginTransaction();
    }

    public async Task<int> CommitTransactionAsync()
    {
        if (Database.CurrentTransaction != null)
            Database.CommitTransaction();

        return await SaveChangesAsync();
    }

    public async Task<int> RollbackTransactionAsync()
    {
        if (Database.CurrentTransaction != null)
            Database.RollbackTransaction();

        return await SaveChangesAsync();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "admin";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "admin";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}