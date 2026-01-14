using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.CommonInterfaces
{
    public interface IEmailServices
    {

        Task SendAsync(string to , string subject, string body);


    }
}
