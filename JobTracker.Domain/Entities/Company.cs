using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobTracker.Domain.Common;

namespace JobTracker.Domain.Entities
{
    public class Company : BaseEntity
    {

        public string Name { get; set; } = null!;
        public string Website { get; set; }
        public string? Industry { get; set; }


    }
}
