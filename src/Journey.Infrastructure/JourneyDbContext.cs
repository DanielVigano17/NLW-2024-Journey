using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure
{
    public class JourneyDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*optionsBuilder.UseSqlite("Data Source=C:\\Users\\Windows\\Documents\\JourneyDatabase.db");*/
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\Daniel\\Documents\\JourneyDatabase.db");
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Activity>().ToTable("Activities");
        }
    }
}
