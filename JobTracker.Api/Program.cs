

using FluentValidation;
using JobTracker.Api.Middleware;
using JobTracker.Application.Auth;
using JobTracker.Application.Auth.Services;
using JobTracker.Application.CommonInterfaces;
using JobTracker.Application.Repository.AuthRepository;
using JobTracker.Application.Validators.AuthValidators;
using JobTracker.Infrastructure.CommonServices;
using JobTracker.Infrastructure.Data;
using JobTracker.Infrastructure.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;

namespace JobTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // DbContext
            builder.Services.AddDbContext<JobTrackerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(builder.Configuration["Redis:Connection"]!));

            // Data protection (AES)
            builder.Services.AddDataProtection();

            // Dependency Injection
            builder.Services.AddScoped<IEmailServices, SmtpEmailService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IVerificationService, VerificationService>();
            builder.Services.AddScoped<IEncryptionService, EncryptionService>();

            // FluentValidation
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

            // JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)
                        )
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("JWT AUTH FAILED:");
                            Console.WriteLine(context.Exception.ToString());
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            Console.WriteLine("JWT CHALLENGE:");
                            Console.WriteLine(context.Error);
                            Console.WriteLine(context.ErrorDescription);
                            return Task.CompletedTask;
                        }
                    };


                });

            // Role-based authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });

            // Swagger + JWT
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JobTracker API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            var app = builder.Build();

            // Seed admin
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<JobTrackerDbContext>();
                DbInitializer.Seed(db);
            }

            // Swagger UI
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Global error handler
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
