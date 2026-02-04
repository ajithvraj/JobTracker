using JobTracker.Application.DTOs.CoreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Core.Services
{
    public interface IJobApplicationService
    {

        Task<int> CreateAsync(int userId,CreateJobApplicationRequestDto dto);
        Task<List<JobApplicationResponseDto>> GetAllAsync(int userId);
        Task<List<JobApplicationResponseDto>> GetFilteredAsync(int userId, JobApplicationfilterDto filter);
       


    }
}
