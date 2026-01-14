using JobTracker.Application.CommonInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestEmailController : ControllerBase
    {

        private readonly IEmailServices _email;

        public TestEmailController(IEmailServices email)
        {
            _email = email;

        }
        [HttpGet("send-email")]
        public async Task<IActionResult> Send()
        {
            await _email.SendAsync(
                "ajithvraj007@gmail.com",
                "JobTracker Test Email",
                "If you received this, SMTP works!"
            );

            return Ok("Email sent");


        }
    }
}
