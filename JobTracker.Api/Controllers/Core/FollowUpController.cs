using JobTracker.Application.Core.Services;
using JobTracker.Application.DTOs.CoreDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Api.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class FollowUpController : ControllerBase
    {

        private readonly IFollowUpService _followUpService;
      
        public FollowUpController(IFollowUpService followUpService)
        {
            _followUpService = followUpService;
        }

        [HttpPost("{jobApplicationId:int}/send")]

        public async Task<IActionResult> SendManualFollowUp(int jobApplicationId)
        {

            var userId = int.Parse(User.FindFirst("UserId")!.Value); 

            await _followUpService.SendFollowUpAsync(jobApplicationId,userId);

            return Ok(new
            {
                message = "Follow-up email sent successfully"

            });

        }
        [HttpPost("{jobApplicationId:int}/followUp")] 

        public async Task<IActionResult> GetFolloUpHistory( int jobApplicationId)
        {
            var result = await _followUpService.GetFollowHistoryAsync(jobApplicationId);

            return Ok(result);

        }
         
        [HttpPatch("{jobApplicationId:int}/UpdateSts")] 


        public async Task<IActionResult>UpdateStatus(int jobApplicationId, UpdateApplicationStatusDto dto)
        {
            await _followUpService.UpdateStatusAsync(jobApplicationId, dto.Status);
            return Ok("Status updated successfully");

        }





    }
}
