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
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
    }
}
