using JobTracker.Application.Auth;
using JobTracker.Application.Common;
using JobTracker.Application.DTOs.AuthDTOs;
using JobTracker.Application.Repository.AuthRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;

namespace JobTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _auth;
       // private readonly IUserRepository _user;
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

        [Authorize(Roles = "User")]
        [HttpPost("email-Stmp")]

         public async Task<IActionResult> SaveEmailSettings(UpdateEmailSettingsDto dto)
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            await _auth.SaveEmailsettingsAsync(userId, dto);
            return Ok("Email settings Saved ");

        }

     }



    
}
