using JobTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Domain.Entities
{
    public class Resume : BaseEntity
    {

       public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string OriginalFileName { get; set; } = null!;
        public string StoredFileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = null!;
    }


}

