using JobTracker.Application.Repository.CoreRepository;
using JobTracker.Domain.Entities;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Services.CoreServices
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly JobTrackerDbContext _db;

        public CompanyRepository(JobTrackerDbContext db)
        {
            _db = db;
        }

        public async Task<Company?> GetByName(string name)
        {
          return await  _db.Companies.FirstOrDefaultAsync(x => x.Name == name);

        }

        public async Task<Company> CreateAsync(string name)
        {
            var company = new Company
            {
                Name = name
            }; 

            _db.Add(company);
            await _db.SaveChangesAsync();
            return company;
             
        }

    }
}
