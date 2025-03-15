using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExecutionStrategy;

public sealed class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DummyTable>(e =>
        {
            e.HasKey(q => q.Id);
            e.HasData(
                //Here's how you could represent the titles and descriptions in a C# parameterized dummy table. This would typically be structured as an array or list of objects:
                new DummyTable { Title = "Echoes of Eternity", Description = "A captivating tale exploring the timeless struggle between fate and free will, set in a mystical realm.", IsPublic = true },
                new DummyTable { Title = "Pixels and Paint", Description = "A guide to blending digital and traditional art techniques for modern creatives.", IsPublic = true },
                new DummyTable { Title = "Whispers from the Hollow", Description = "A chilling horror story where secrets buried in a forgotten village come back to haunt.", IsPublic = false },
                new DummyTable { Title = "Unlocking Innovation", Description = "A handbook for leaders on fostering creativity and groundbreaking ideas in teams.", IsPublic = false },
                new DummyTable { Title = "Stardust Beneath Our Feet", Description = "A poetic journey connecting astronomy to the human spirit and our place in the cosmos.", IsPublic = true },
                new DummyTable { Title = "The Culinary Symphony", Description = "An adventurous cookbook featuring recipes inspired by the world's most beloved classical compositions.", IsPublic = false },
                new DummyTable { Title = "Through the Prism of Time", Description = "A science fiction novel following a group of time travelers grappling with the paradoxes of their mission.", IsPublic = true },
                new DummyTable { Title = "The Green Blueprint", Description = "An in-depth look at sustainable living practices for a better tomorrow.", IsPublic = false },
                new DummyTable { Title = "Threads of Resilience", Description = "A heartwarming memoir chronicling personal stories of overcoming life's challenges.", IsPublic = true },
                new DummyTable { Title = "The Social Alchemy", Description = "An exploration of how digital platforms shape human behavior and societal trends.", IsPublic = false }
            );
        });
    }
}
