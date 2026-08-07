using DevBooking.Domain.Entities;
using DevBooking.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<DeveloperProfile> DeveloperProfiles { get; set; } = null!;
        public DbSet<ClientProfile> ClientProfiles { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Follow> Follows => Set<Follow>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<AvailabilitySlot>()
                 .Property(s => s.RowVersion)
                 .IsRowVersion();

            modelBuilder.Entity<Review>()
                .HasOne(r => r.DeveloperProfile)
                .WithMany()
                .HasForeignKey(r => r.DeveloperProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Booking)
                .WithOne()
                .HasForeignKey<Review>(r => r.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
