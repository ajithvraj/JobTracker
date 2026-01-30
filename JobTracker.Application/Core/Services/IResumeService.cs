using JobTracker.Application.DTOs.CoreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Core.Services
{
    public interface IResumeService
    {

        Task<UploadResumeResponseDto> UploadAsync(int userId, UploadResumeRequestDto dto);
       
       

    }
}
