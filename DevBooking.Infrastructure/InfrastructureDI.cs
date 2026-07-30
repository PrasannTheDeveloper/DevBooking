using DevBooking.Application.Interfaces;
using DevBooking.Infrastructure.Identity;
using DevBooking.Infrastructure.Persistence;
using DevBooking.Infrastructure.Persistence.Repositories;
using DevBooking.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevBooking.Infrastructure.Files;

namespace DevBooking.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service , IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            service.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IDeveloperProfileRepository, DeveloperProfileRepository>();
            service.AddScoped<IDeveloperProfileService, DeveloperProfileService>();
            service.AddScoped<IServiceRepository, ServiceRepository>();
            service.AddScoped<IServiceManagementService, ServiceManagementService>();
            service.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();
            service.AddScoped<IAvailabilityService, AvailabilityService>();
            service.AddScoped<IBookingRepository, BookingRepository>();
            service.AddScoped<IBookingService, BookingService>();
            service.AddScoped<IFileStorageService, LocalFileStorageService>();
            
            return service;
        }
    }
}
