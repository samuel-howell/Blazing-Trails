using BlazingTrails.Api.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Persistence
{
    public class BlazingTrailsContext : DbContext // The DbContext class provides all the base functionality for the context. All databasecontexts must inherit from this class
    {
        public DbSet<Trail> Trails => Set<Trail>(); // Each entity is represented as a collection with the type DbSet<T>. These are essentially repositories
        public DbSet<RouteInstruction> RouteInstructions => Set<RouteInstruction>();

        public BlazingTrailsContext(DbContextOptions<BlazingTrailsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TrailConfig()); // By overriding the OnModelCreating method, we can hook up the entity configurationclasses we created in the previous section.
            modelBuilder.ApplyConfiguration(new RouteInstructionConfig());
        }
    }
}
