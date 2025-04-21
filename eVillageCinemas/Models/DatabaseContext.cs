using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace eVillageCinemas.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasOne(s => s.Cinema)
                .WithMany(g => g.Movies)
                .HasForeignKey(s => s.CinemaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AvailableDate>()
                .HasOne(s => s.Movie)
                .WithMany(g => g.AvailableDates)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AvailableDate>()
                .HasOne(s => s.Hall)
                .WithMany(g => g.AvailableDates)
                .HasForeignKey(s => s.HallId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AvailableSeat>()
                .HasOne(s => s.AvailableDate)
                .WithMany(g => g.AvailableSeats)
                .HasForeignKey(s => s.AvailableDateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Hall)
                .WithMany(g => g.Seats)
                .HasForeignKey(s => s.HallId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(s => s.Order)
                .WithMany(g => g.Tickets)
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(s => s.AvailableSeat)
                .WithMany(g => g.Tickets)
                .HasForeignKey(s => s.AvailableSeatId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(s => s.Payment)
                .WithOne(g => g.Order)
                .HasForeignKey<Order>(s => s.PaymentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(s => s.Order)
                .WithOne(g => g.Payment)
                .HasForeignKey<Payment>(s => s.OrderId);
        }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<AvailableDate> AvailableDates { get; set; }

        public DbSet<AvailableSeat> AvailableSeats { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
