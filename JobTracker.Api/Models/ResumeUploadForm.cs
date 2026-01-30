using Microsoft.AspNetCore.Http;
namespace JobTracker.Api.Models
{
    public class ResumeUploadForm
    {
        public IFormFile File { get; set; } = null!;
    }
}
