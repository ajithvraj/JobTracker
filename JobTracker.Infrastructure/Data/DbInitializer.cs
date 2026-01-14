using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Data
{
    public static class DbInitializer
    {

        public static void Seed(JobTrackerDbContext context)
        {
            if(!context.Users.Any())
            {

                var admin = new User
                {

                    FullName = "admin",
                    Email = "admin@jotracker",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = "Admin"

                };

                context.Users.Add(admin);
                context.SaveChanges();

            }

        }
            

    }
}
