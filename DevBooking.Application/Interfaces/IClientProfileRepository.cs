using DevBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Application.Interfaces
{
    public interface IClientProfileRepository
    {
        Task<ClientProfile?> GetByIdAsync(int id);
        Task<ClientProfile?> GetByUserIdAsync(string userId);
        Task<List<ClientProfile>> GetAllAsync();
        Task AddAsync(ClientProfile profile);
        Task SaveChangesAsync();
    }
}
