using JobTracker.Api.Models;
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
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _service;

       public ResumeController(IResumeService service)
        {
            _service = service;
        
        }

        [HttpPost("Upload")]
        [Consumes("multipart/form-data")]


        public async Task<IActionResult> Upload([FromForm] ResumeUploadForm form)
        {
            var file = form.File;
            var userId = int.Parse(User.FindFirst("UserId")!.Value);

            var dto = new UploadResumeRequestDto
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileSize = file.Length,
                FileStream = file.OpenReadStream()

            };

            var result = await _service.UploadAsync(userId, dto);
            return Ok(result);


        }

    }
}
