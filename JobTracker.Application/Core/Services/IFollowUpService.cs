using JobTracker.Application.DTOs.CoreDTOs.FollowUpDTOs;
using JobTracker.Domain.Enums.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Core.Services
{
    public interface IFollowUpService
    {
        Task SendFollowUpAsync(int jobApplicationId, int userId);
        Task<List<FollowUpHistoryDto>> GetFollowHistoryAsync(int jobApplicationId);
        Task UpdateStatusAsync(int jobApplicationId, ApplicationStatus status);

    }
}
