using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            //for windows -----------------
            //optionsBuilder.UseSqlServer
            // ("Server=(localdb)\\mssqllocaldb;Database=DevBookingApp;Trusted_Connection=True;MultipleActiveResultSets=true");

            //linux -
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=DevBooking;User Id=sa;Password=Prasann@123;TrustServerCertificate=True;Encrypt=False;");
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
