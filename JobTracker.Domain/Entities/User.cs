using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobTracker.Domain.Common;

namespace JobTracker.Domain.Entities
{
    public class User : BaseEntity
    {

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
        public string? SmtpPassword { get; set; }

        public string Role { get; set; } = "Admin";


    }
}
