using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs.CoreDTOs
{
    public class JobApplicationfilterDto
    { 
        public int? Status {  get; set; } 
        public int? Source {  get; set; }

        public DateTime? FromDate { get; set; } 
        public DateTime? ToDate { get; set; }




    }
}
