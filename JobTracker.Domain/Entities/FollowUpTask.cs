using JobTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Domain.Entities
{
    public class FollowUpTask : BaseEntity
    {

        public int JobApplicationId { get; set; }
        public JobApplication? JobApplication { get; set; }

        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public string TemplateUsed { get; set; } = null!;


    }
}
