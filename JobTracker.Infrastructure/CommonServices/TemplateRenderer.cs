using JobTracker.Application.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.CommonServices
{
    public class TemplateRenderer : ITemplateRenderer
    {

      public  string Render(string template, Dictionary<string, string> values) 
        {

            foreach (var kv in values)
            {

                template = template.Replace($"{{{{{kv.Key}}}}}", kv.Value);


            }

            return template;
        
        }

    }
}
