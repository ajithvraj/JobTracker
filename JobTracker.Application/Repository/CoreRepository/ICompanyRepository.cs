using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Repository.CoreRepository
{
    public interface ICompanyRepository
    {

        Task<Company?> GetByName(string name);
        Task<Company> CreateAsync(string name);




    }
}
