using DevBooking.Application.Interfaces;
using DevBooking.Application.Services;
using DevBooking.Domain.Entities;
using DevBooking.Infrastructure.Files;
using DevBooking.Infrastructure.Identity;
using DevBooking.Infrastructure.Persistence;
using DevBooking.Infrastructure.Persistence.Repositories;
using DevBooking.Infrastructure.Repositories;
using DevBooking.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            service.AddScoped<IClientProfileRepository, ClientProfileRepository>();
            service.AddScoped<IClientProfileService, ClientProfileService>();
            service.AddScoped<IReviewRepository, ReviewRepository>();
            service.AddScoped<IReviewService, ReviewService>();
            service.AddScoped<IFollowRepository, FollowRepository>();
            service.AddScoped<IFollowService, FollowService>();
            service.AddScoped<INotificationRepository, NotificationRepository>();
            service.AddScoped<INotificationService, NotificationService>();
            return service;
        }
    }
}
