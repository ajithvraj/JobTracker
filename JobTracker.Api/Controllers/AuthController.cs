using JobTracker.Application.Auth;
using JobTracker.Application.Common;
using JobTracker.Application.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _auth; 

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }
       
      

            [HttpPost("register")]
            public async Task<IActionResult> Register(RegisterRequestDto dto)
            {
                await _auth.RegisterAsync(dto);
                return Ok("Verification email sent");
            }

            [HttpGet("verify")]
            public async Task<IActionResult> Verify([FromQuery] VerifyEmailRequestDto dto)
            {
                await _auth.VerifyEmailAsync(dto);
                return Ok("Email verified. You can now login.");
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(LoginRequestDto dto)
            {
                var result = await _auth.LoginAsync(dto);
                return Ok(ApiResponse<AuthResponseDto>.Ok(result, "Logged in"));
            }
        }



    
}
