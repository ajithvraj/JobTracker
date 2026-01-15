using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobTracker.Application.CommonInterfaces;
using Microsoft.AspNetCore.DataProtection;

namespace JobTracker.Infrastructure.CommonServices
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IDataProtector _protector;



        public EncryptionService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("JobTracker.Stmp");

        }

        public string Encrypt(string value)
        {
            return _protector.Protect(value);

        }

        public string Decrypt(string value) {

            return _protector.Unprotect(value);
        }


    }
}
