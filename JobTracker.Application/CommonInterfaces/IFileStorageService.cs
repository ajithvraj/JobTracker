using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.CommonInterfaces
{
    public interface IFileStorageService
    {

        Task<string> SaveAsync(
    int userId,
    Stream fileStream,
    string originalFileName
);

    }
}
