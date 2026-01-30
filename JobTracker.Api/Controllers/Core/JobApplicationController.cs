using JobTracker.Application.Core.Services;
using JobTracker.Application.DTOs.CoreDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Api.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class JobApplicationController : ControllerBase
    {

        private readonly IJobApplicationService _service;

       public JobApplicationController ( IJobApplicationService service)
        {
            _service = service;
        }

        [HttpPost] 

        public async Task<IActionResult> Create (CreateJobApplicationRequestDto dto)
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            var id = await _service.CreateAsync(userId, dto); 
            return Ok (new {Id = id});

        }

        [HttpGet("My")] 

        public async Task<IActionResult> GetMyApplications()
        {

            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            return Ok(await _service.GetAllAsync(userId));

        }

        [HttpGet] 

        public async Task<IActionResult> Get([FromQuery] JobApplicationfilterDto filter)
        {

            var userId = int.Parse(User.FindFirst("UserId")!.Value);

            var result = await _service.GetFilteredAsync(userId, filter);
            return Ok (result);

        }
    }
}
