using JobTracker.Domain.Common;
using JobTracker.Domain.Enums;
using JobTracker.Domain.Enums.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Domain.Entities
{
    public class JobApplication : BaseEntity
    {

        public int UserId { get; set; } 
        public User? User { get; set; }
         
        public int CompanyId { get; set; } 
        public Company? Company { get; set; } 

        public int RecruiterId { get; set; } 
        public Recruiter? Recruiter { get; set; }


        public string? Role { get; set; } = null!;
        public ApplicationSource Source { get; set; }
        public DateTime AppliedDate { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied; 

        public String? ResumeVersion {  get; set; } 
        public DateTime? LastContactDate { get; set; }





    }
}
