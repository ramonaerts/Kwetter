using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementService.Entities;
using FileManagementService.Enums;
using MongoDB.Driver;

namespace FileManagementService.DAL
{
    public class FileManagementRepository : IFileManagementRepository
    {
        private readonly IFileManagementContext _context;

        public FileManagementRepository(IFileManagementContext context)
        {
            _context = context;
        }

        public ProfileImage GetImage(string imageName, DataType dataType)
        {
            var profileImages = _context.ProfileImages.AsQueryable().ToList();

            return profileImages.FirstOrDefault(p => p.Filename == imageName && p.DataType == dataType);
        }

        public async Task SaveImage(ProfileImage profileImage)
        {
            await _context.ProfileImages.InsertOneAsync(profileImage);
        }
    }
}
