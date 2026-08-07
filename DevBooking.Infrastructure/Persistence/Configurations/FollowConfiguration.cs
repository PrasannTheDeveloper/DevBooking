using DevBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevBooking.Infrastructure.Persistence.Configurations;

public class FollowConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.ToTable("Follows");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.FollowerId)
            .IsRequired();

        builder.Property(f => f.FollowingId)
            .IsRequired();

        // Prevent duplicate follow rows (same follower->following pair twice)
        builder.HasIndex(f => new { f.FollowerId, f.FollowingId })
            .IsUnique();

        // Speeds up "who follows user X" queries
        builder.HasIndex(f => f.FollowingId);
    }
}