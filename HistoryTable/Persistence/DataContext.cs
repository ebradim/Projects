using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HistoryTable.Persistence;

public sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<UserCourse> UserCourse => Set<UserCourse>();
    public DbSet<Archive> Archives => Set<Archive>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Archive>(e =>
        {
            e.HasKey(e => e.Id);
            e.ToTable("Archives");
        }).Entity<Course>(e =>
        {
            e.HasKey(e => e.Id);
            e.ToTable("Courses");
        }).Entity<UserCourse>(e =>
        {
            e.HasKey(e => new { e.UserId, e.CourseId });
            e.ToTable("UserCourses");
        })
        .HasPostgresEnum<ArchiveEvent>();

    }
}
