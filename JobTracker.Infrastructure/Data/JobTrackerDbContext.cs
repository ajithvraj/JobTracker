using JobTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Data
{
    public class JobTrackerDbContext : DbContext
    {

        public JobTrackerDbContext (DbContextOptions<JobTrackerDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Company> Companies => Set<Company>(); 
        public DbSet<Recruiter> Recruiters => Set<Recruiter>();
        public DbSet<JobApplication> JobApplications => Set<JobApplication>();
        public DbSet<EmailLog> EmailLogs => Set<EmailLog>(); 
        public DbSet<FollowUpTask> FollowUpTasks => Set<FollowUpTask>();
        public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Recruiter -> Company   

            modelBuilder.Entity<Recruiter>()
                .HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            // JobApplication  -> Company 

            modelBuilder.Entity<JobApplication>()
                .HasOne(j => j.Company)
                .WithMany()
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);


            // JobApplication  -> Recruiter 

            modelBuilder.Entity<JobApplication>()
                .HasOne(j => j.Recruiter)
                .WithMany()
                .HasForeignKey(j => j.RecruiterId)
                 .OnDelete(DeleteBehavior.Restrict);

            // JobApplication  -> User 

            modelBuilder.Entity<JobApplication>()
                .HasOne(j => j.User)
                .WithMany()
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // EmailLog → JobApplication
            modelBuilder.Entity<EmailLog>()
                .HasOne(e => e.JobApplication)
                .WithMany()
                .HasForeignKey(e => e.JobApplicationId);


            // FollowUpTask → JobApplication
            modelBuilder.Entity<FollowUpTask>()
                .HasOne(f => f.JobApplication)
                .WithMany()
                .HasForeignKey(f => f.JobApplicationId);










        }

    }
}
