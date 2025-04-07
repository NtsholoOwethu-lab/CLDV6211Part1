using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EventEase.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Venue> Venue { get; set; }

        public DbSet<EventDetails> EventDetails { get; set; }

        public DbSet<Bookings> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly configure the relationship between Bookings and EventDetails
            modelBuilder.Entity<Bookings>()
                .HasOne(b => b.EventDetails)  // Navigation property to EventDetails
                .WithOne()  // Assuming EventDetails has a one-to-one relationship with Bookings
                .HasForeignKey<Bookings>(b => b.EventId)  // Foreign key in the Bookings table
                .HasConstraintName("FK_Bookings_EventDetails"); // Optional: name for the constraint
        }
    }
}
