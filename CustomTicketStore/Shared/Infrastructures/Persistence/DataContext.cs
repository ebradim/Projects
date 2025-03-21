using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CustomTicketStore.Shared.Infrastructures.Persistence;

public sealed class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
            .HasPostgresExtension("uuid-ossp");
        // .HasPostgresExtension("postgis");

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }

}
