using JobTracker.Application.Core.Services;
using JobTracker.Application.Repository.CoreRepository;
using JobTracker.Domain.Enums.ApplicationEnums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.BackgroundJobs
{


    public class FollowUpScheduler : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<FollowUpScheduler> _logger;

        public FollowUpScheduler(
            IServiceScopeFactory scopeFactory,
            ILogger<FollowUpScheduler> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessFollowUps();
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private async Task ProcessFollowUps()
        {
            using var scope = _scopeFactory.CreateScope();

            var repo = scope.ServiceProvider.GetRequiredService<IJobApplicationRepository>();
            var followUpService = scope.ServiceProvider.GetRequiredService<IFollowUpService>();

            var dueDate = DateTime.UtcNow.AddDays(-5);

            var applications = await repo.GetDueEmailFollowUpsAsync(dueDate);

            foreach (var app in applications)
            {
                try
                {
                    await followUpService.SendFollowUpAsync(app.Id, app.UserId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Follow-up failed for JobApplication {app.Id}");
                }
            }
        }
    }


}
