using JobTracker.Application.CommonInterfaces;
using JobTracker.Application.DTOs.AuthDTOs;
using JobTracker.Application.Repository.AuthRepository;
using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace JobTracker.Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _user;
        private readonly ITokenService _token;
        private readonly IVerificationService _verify;
        private readonly IEncryptionService _encrypt;

        public AuthService (IUserRepository user, ITokenService token,IVerificationService verify, IEncryptionService encrypt )
        {
           _user = user;
            _token = token;
            _verify = verify;
            _encrypt = encrypt;
        }
        public async Task RegisterAsync(RegisterRequestDto dto)
        {
            var existing = await _user.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new Exception("Email already registered");

            await _verify.CreatePendingUserAsync(dto);
        }

        public async Task VerifyEmailAsync(VerifyEmailRequestDto dto)
        {
            var pending = await _verify.VerifyAsync(dto.Email, dto.Token);

            if (pending == null)
                throw new Exception("Invalid or expired verification link");

            var user = new User
            {
                FullName = pending.FullName,
                Email = pending.Email,
                PasswordHash = pending.PasswordHash,
                Role = "User"
            };

            await _user.AddAsync(user);
            await _user.SaveAsync();
        }

        public async Task<AuthResponseDto>LoginAsync(LoginRequestDto dto)
        {
            var user = await _user.GetByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            var jwt = _token.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = jwt,
                Email = user.Email,
                Role = user.Role
            };
        }


        public async Task SaveEmailsettingsAsync( int userId , UpdateEmailSettingsDto dto )
        {

            var user = await _user.GetByIdAsync(userId); 
            if(userId == null)
            {
                throw new Exception("User not found");

            }

            user.SmtpPassword = _encrypt.Encrypt(dto.Smtpassword);
            await _user.SaveAsync();

        }


    }
}
