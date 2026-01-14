using JobTracker.Application.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Repository.AuthRepository
{
    public interface IVerificationService
    {

        Task CreatePendingUserAsync(RegisterRequestDto dto);
        Task<PendingUserDto?> VerifyAsync(string email, string token);
    }
}
