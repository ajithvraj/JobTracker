using JobTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Domain.Entities
{
    public class Recruiter : BaseEntity
    {

        public string? Name { get; set; } 
        public string Email { get; set; } = null!; 
        public string? Phone { get; set; } 

        public int CompanyId { get; set; } 
        public Company? Company { get; set; }





    }
}
