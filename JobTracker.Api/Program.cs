
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
using StackExchange.Redis;
using System.Text;

namespace JobTracker.Api

{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<JobTrackerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IEmailServices, SmtpEmailService>();
            builder.Services.AddScoped<ITokenService, TokenService>(); 
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            builder.Services.AddScoped<IAuthService,AuthService>();
            builder.Services.AddScoped<IVerificationService,VerificationService>();




            //Validators assembly scanning
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

            //Redis Registration

            builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(builder.Configuration["Redis:Connection"]!)
);




            var app = builder.Build();

            //JWT settings  
            //var jwtSettings = builder.Configuration.GetSection("JwtSettings");

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,

            //        ValidIssuer = jwtSettings["Issuer"],
            //        ValidAudience = jwtSettings["Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            //        )
            //    };
            //});



            //this block of code make sure the db perfectly initialized like seeding admin as the first data  

            using (var scope = app.Services.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<JobTrackerDbContext>();
                DbInitializer.Seed(db);

            }


                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

            app.UseHttpsRedirection();

            app.UseMiddleware<GlobalExceptionMiddleware>();


            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
