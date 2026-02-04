using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.CommonInterfaces
{
    public interface ITemplateRenderer
    {
        string Render(string template , Dictionary<string,string> values);
    }
}
