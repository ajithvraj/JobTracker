using JobTracker.Application.DTOs.CoreDTOs;
using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Repository.CoreRepository
{
    public interface IJobApplicationRepository
    {
        Task<int> CreateAsync(JobApplication application);
        Task<List<JobApplicationResponseDto>> GetAllByUserAsync(int userId);
        Task<JobApplication> GetWithDetailsAsync(int jobApplicationId);
        Task UpdateAsync(JobApplication application);
        Task<List<JobApplication>> GetDueEmailFollowUpsAsync(DateTime dueDate);

    }
}
