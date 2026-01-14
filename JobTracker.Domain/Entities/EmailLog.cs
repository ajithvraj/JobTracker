using JobTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Domain.Entities
{
    public class EmailLog : BaseEntity
    { 
        public int JobApplicationId { get; set; } 
        public JobApplication? JobApplication { get; set; }

        public string ToEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!; 

        public DateTime SentAt { get; set; } 

        public bool IsOpened { get; set; } 
        public bool IsReplied { get; set; }

    }
}
