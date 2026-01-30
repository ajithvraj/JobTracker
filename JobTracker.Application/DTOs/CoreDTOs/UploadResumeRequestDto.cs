using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs.CoreDTOs
{
    public class UploadResumeRequestDto
    {

        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public Stream FileStream { get; set; } = null!;


    }
}
