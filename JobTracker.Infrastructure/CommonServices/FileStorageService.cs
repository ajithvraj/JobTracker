using JobTracker.Application.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace JobTracker.Infrastructure.CommonServices
{

    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env; 

        public FileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveAsync(
    int userId,
    Stream fileStream,
    string originalFileName)
        {

            // Build a folder path like:
            // <ProjectRoot>/Resumes/<UserId>


            var folder = Path.Combine(_env.ContentRootPath, "Resumes", userId.ToString());



            // Create the directory if it does not already exist
            Directory.CreateDirectory(folder);

            //Generate a unique filename to avoid overwriting files
        // Always saving as PDF

            var storageName = $"{Guid.NewGuid()}.pdf";

            // Full absolute file path where the resume will be stored
            var fullPath = Path.Combine(folder, storageName);

            // Create a FileStream to write the file to disk
            using var output = new FileStream(fullPath, FileMode.Create);
            await fileStream.CopyToAsync(output);

            return fullPath;



        }





    }


}
