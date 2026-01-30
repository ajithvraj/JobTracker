using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Repository.CoreRepository
{
    public interface IResumeRepository
    {

        Task AddResumeAsync(Resume resume);
        Task<List<Resume>> GetByUserIdAsync(int userId);


    }
}
