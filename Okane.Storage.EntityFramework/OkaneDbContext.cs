using Microsoft.EntityFrameworkCore;
using Okane.Application;
using Okane.Domain;

namespace Okane.Storage.EntityFramework;

public class OkaneDbContext(DbContextOptions<OkaneDbContext> options) : DbContext(options)
{
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OkaneDbContext).Assembly);
}