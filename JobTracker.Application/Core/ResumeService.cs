using JobTracker.Application.CommonInterfaces;
using JobTracker.Application.Core.Services;
using JobTracker.Application.DTOs.CoreDTOs;
using JobTracker.Application.Repository.CoreRepository;
using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Core
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _repo;
        private readonly IFileStorageService _storage;
        

        public ResumeService(IResumeRepository repo, IFileStorageService storage  )
        {
            _repo = repo;
            _storage = storage;
            
        }

       public async Task<UploadResumeResponseDto> UploadAsync(int userId, UploadResumeRequestDto dto)
        {

            //business logic 

            if (dto.ContentType != "application/pdf")
                throw new ArgumentException("Only pdf allowed");

            if (dto.FileSize > 2 * 1024 * 1024) throw new ArgumentException("Max size is 2MB");

            //save file (ifrastructure) 

            var storagePath = await _storage.SaveAsync(userId, dto.FileStream, dto.FileName);



            //map dto to entity 

            var resume = new Resume
            {
                UserId = userId,
                FileSize = dto.FileSize,
                OriginalFileName = dto.FileName,
                StoredFileName = Path.GetFileName(storagePath),
                FilePath = storagePath,
                ContentType = dto.ContentType,
                

            };

            //persist 


            await _repo.AddResumeAsync(resume);

            return new UploadResumeResponseDto
            {
                ResumeId = resume.Id,
                FileName = resume.OriginalFileName

            };





        }


    }
}
