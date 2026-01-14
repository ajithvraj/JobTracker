using JobTracker.Application.CommonInterfaces;
using JobTracker.Application.DTOs.AuthDTOs;
using JobTracker.Application.Repository.AuthRepository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace JobTracker.Infrastructure.Services.AuthServices
{
    public class VerificationService : IVerificationService
    {
        private readonly IDatabase _redis;
        private readonly IEmailServices _email;



        public VerificationService(IConnectionMultiplexer redis, IEmailServices email)
        {
            _redis = redis.GetDatabase();
            _email = email;
        }


        public async Task CreatePendingUserAsync(RegisterRequestDto dto)
        {
            var token = Guid.NewGuid().ToString("N");

            var pending = new PendingUserDto
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Token = token
            };

            var json = JsonSerializer.Serialize(pending);

            await _redis.StringSetAsync(
                $"pending:{dto.Email}",
                json,
                TimeSpan.FromMinutes(10)
            );

            //var link = $"https://abcd-12-34-56.ngrok.io/api/auth/verify\r\n={dto.Email}&token={token}";
            //var link = $"https:///uncardinally-nondropsical-jayse.ngrok-free.dev/api/auth/verify?email={dto.Email}&token={token}";
            var link = $"https://uncardinally-nondropsical-jayse.ngrok-free.dev/api/auth/verify?email={dto.Email}&token={token}";


            await _email.SendAsync(dto.Email, "Verify your JobTracker account", link);
        }

        public async Task<PendingUserDto?> VerifyAsync(string email, string token)
        {
            var json = await _redis.StringGetAsync($"pending:{email}");
            if (json.IsNullOrEmpty) return null;

            var pending = JsonSerializer.Deserialize<PendingUserDto>(json!);

            if (pending!.Token != token)
                return null;

            await _redis.KeyDeleteAsync($"pending:{email}");
            return pending;
        }
    }

    

   
}
