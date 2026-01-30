using JobTracker.Domain.Enums.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs.CoreDTOs
{
    public class CreateJobApplicationRequestDto
    {

        public string CompanyName { get; set; } = null!;   // New or existing
        public string? RecruiterName { get; set; }          // Optional
        public string? RecruiterEmail { get; set; }         // Optional

        public string Role { get; set; } = null!;
        public ApplicationSource Source { get; set; }
        public DateTime AppliedDate { get; set; }

        public string ResumeVersion { get; set; } = null!;  // FullStack / DotNet
        public string? Notes { get; set; }

    }
}
