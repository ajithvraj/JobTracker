using JobTracker.Application.DTOs.CoreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Repository.CoreRepository
{
    public interface IApplicationStatus
    {

      Task<List<JobApplicationResponseDto>> GetFilterAsync(int userId, JobApplicationfilterDto filter);


    }
}
