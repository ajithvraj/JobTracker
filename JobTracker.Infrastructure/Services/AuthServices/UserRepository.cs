using JobTracker.Application.Repository.AuthRepository;
using JobTracker.Domain.Entities;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Services.AuthServices
{
    public class UserRepository : IUserRepository
    {

        private readonly JobTrackerDbContext _db; 

        public UserRepository (JobTrackerDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {

            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

        }

        public async Task AddAsync(User user)
        {

            await _db.Users.AddAsync(user);

        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
