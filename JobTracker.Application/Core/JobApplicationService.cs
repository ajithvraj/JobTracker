using JobTracker.Application.Core.Services;
using JobTracker.Application.DTOs.CoreDTOs;
using JobTracker.Application.Repository.CoreRepository;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Core
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _applicatio;
        private readonly IRecruiterRepository _recruiter;
        private readonly ICompanyRepository _company;
        private readonly IApplicationStatus _sts;


        public JobApplicationService
            (

            IJobApplicationRepository applicatio,
            IRecruiterRepository recruiter ,
            ICompanyRepository company,
            IApplicationStatus sts
            
            )
        {
            _applicatio = applicatio;
            _recruiter = recruiter;
            _company = company;
            _sts = sts;

        }

        public async Task<int>CreateAsync(int userId, CreateJobApplicationRequestDto dto)
        {

            //checking company 

            var company = await _company.GetByName(dto.CompanyName) ?? await _company.CreateAsync(dto.CompanyName);

            //recruiter Optional, not every jobplatforms have recruiters only for company carreer portal  and personal email 

            int? recruiterId = null; 
            if(!string.IsNullOrWhiteSpace(dto.RecruiterEmail))
            {
                var recruiter = await _recruiter.GetByEmail(dto.RecruiterEmail) ?? await _recruiter.CreateAsync
                    (

                    dto.RecruiterName,
                    dto.RecruiterEmail,
                    company.Id

                    );

                recruiterId = recruiter.Id;

            }


            //job Application 

            var application = new JobApplication
            {
                UserId = userId,
                CompanyId = company.Id,
                RecruiterId = recruiterId, 
                Role = dto.Role,
                Source = dto.Source,
                AppliedDate = dto.AppliedDate,
                ResumeVersion = dto.ResumeVersion,
                Status = Domain.Enums.ApplicationEnums.ApplicationStatus.Applied

              



            };
           

            return await _applicatio.CreateAsync(application);



        }

       public async Task<List<JobApplicationResponseDto>> GetAllAsync(int userId)
        {
            return await _applicatio.GetAllByUserAsync(userId);

    


        }
        public async Task<List<JobApplicationResponseDto>> GetFilteredAsync(
    int userId,
    JobApplicationfilterDto filter)
        {
            return await _sts.GetFilterAsync(userId, filter);
        }



    }




}
