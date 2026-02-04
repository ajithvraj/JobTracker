using Dapper;
using JobTracker.Application.DTOs.CoreDTOs;
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
    public class ApplicationStatus : IApplicationStatus
    {

        private readonly JobTrackerDbContext _db; 

        public ApplicationStatus (JobTrackerDbContext db)
        {
            _db = db;
        }

        public async Task<List<JobApplicationResponseDto>> GetFilterAsync(int userId, JobApplicationfilterDto filter)
        {
          using var conn = _db.Database.GetDbConnection();

            return (await conn.QueryAsync<JobApplicationResponseDto>
                (

                "GetJobApplicationsFiltered", new
                {
                    UserId = userId,
                    status = filter.Status,
                    FromDate = filter.FromDate,
                    ToDate = filter.ToDate,

                }, commandType: System.Data.CommandType.StoredProcedure

                )).ToList();




        }

        public async Task<JobApplication?> GetByIdAsync(int id)
        {
            return await _db.JobApplications.FindAsync(id); 

        }

        public async Task UpdateAsync(JobApplication app)
        {

            _db.JobApplications.Update(app);
            await _db.SaveChangesAsync();

        }

    }
}
