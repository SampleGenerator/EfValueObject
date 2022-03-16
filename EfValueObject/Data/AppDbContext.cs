using EfValueObject.Models;
using Microsoft.EntityFrameworkCore;

namespace EfValueObject.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Persons = Set<Person>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public DbSet<Person> Persons { get; private set; }
}
