using JobTracker.Domain.Enums.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs.CoreDTOs
{
    public class JobApplicationResponseDto
    {

        public int Id { get; set; }

        public string CompanyName { get; set; } = null!;
        public string? RecruiterName { get; set; }
        public string? RecruiterEmail { get; set; }

        public string Role { get; set; } = null!;
        public ApplicationSource Source { get; set; }
        public ApplicationStatus Status { get; set; }

        public DateTime AppliedDate { get; set; }
        public int DaysSinceApplied { get; set; }

        public string ResumeVersion { get; set; } = null!;
        public DateTime? LastContactDate { get; set; }





    }
}
