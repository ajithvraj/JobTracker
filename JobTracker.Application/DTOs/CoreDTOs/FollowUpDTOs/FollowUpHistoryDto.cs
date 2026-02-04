using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs.CoreDTOs.FollowUpDTOs
{
    public class FollowUpHistoryDto
    {

        public DateTime SentAt { get; set; }
        public string ToEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;


    }
}
