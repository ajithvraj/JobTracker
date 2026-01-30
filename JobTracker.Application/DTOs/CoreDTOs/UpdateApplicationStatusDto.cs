using JobTracker.Domain.Enums.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs.CoreDTOs
{
    public class UpdateApplicationStatusDto
    {

        public ApplicationStatus Status { get; set; }
        public DateTime? LastContactDate { get; set; }
    }
}
