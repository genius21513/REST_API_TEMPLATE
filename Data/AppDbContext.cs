using Microsoft.EntityFrameworkCore;
using REST_API_TEMPLATE.Models;

namespace REST_API_TEMPLATE.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Define relatinoship between images and albums
            builder.Entity<Image>()
                .HasOne(x => x.Album)
                .WithMany(x => x.Images);

            // Seed database with authors and books for demo
            new DbInitializer(builder).Seed();
        }
    }
}
