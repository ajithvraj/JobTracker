using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.CommonInterfaces
{
    public interface IEncryptionService
    {

        string Encrypt(string Value); 
        string Decrypt(string Value);


    }
}
