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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // MUST come first — Identity needs to configure its own tables

            modelBuilder.Entity<AvailabilitySlot>()
                .Property(s => s.RowVersion)
                .IsRowVersion();
        }
    }

}
