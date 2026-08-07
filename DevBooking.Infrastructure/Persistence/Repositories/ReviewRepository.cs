using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
        }

        public async Task DeleteAsync(Review review)
        {
            _context.Reviews.Remove(review);
        }

        public async Task<double> GetAverageRatingAsync(int developerProfileId)
        {
            var reviews = await _context.Reviews.Where(r => r.DeveloperProfileId == developerProfileId).ToListAsync();
            if(reviews.Count == 0)
            {
                return 0;
            }
            return reviews.Average(r=>r.Rating);
        }

        public async Task<Review?> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.BookingId == bookingId);
        }

        public async Task<List<Review>> GetByDeveloperProfileIdAsync(int developerProfileId)
        {
            return await _context.Reviews.Where(r => r.DeveloperProfileId == developerProfileId).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<int> GetReviewCountAsync(int developerProfileId)
        {
            return await _context.Reviews.CountAsync(r => r.DeveloperProfileId == developerProfileId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<string>> GetFollowerIdsAsync(string followingId, CancellationToken ct)
        {
            return await _context.Follows
                .Where(f => f.FollowingId == followingId)
                .Select(f => f.FollowerId)
                .ToListAsync(ct);
        }
    }
}
