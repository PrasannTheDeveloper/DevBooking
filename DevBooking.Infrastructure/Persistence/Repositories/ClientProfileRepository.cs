using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevBooking.Infrastructure.Persistence.Repositories
{
    public class ClientProfileRepository : IClientProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public ClientProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ClientProfile profile)
        {
            await _context.ClientProfiles.AddAsync(profile);
        }

        public async Task<List<ClientProfile>> GetAllAsync()
        {
            return await _context.ClientProfiles.ToListAsync();
        }

        public async Task<ClientProfile?> GetByIdAsync(int id)
        {
            return await _context.ClientProfiles.FindAsync(id);
        }

        public async Task<ClientProfile?> GetByUserIdAsync(string userId)
        {
            return await _context.ClientProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
