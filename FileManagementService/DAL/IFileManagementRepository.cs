using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementService.Entities;
using FileManagementService.Enums;

namespace FileManagementService.DAL
{
    public interface IFileManagementRepository
    {
        ProfileImage GetImage(string imageName, DataType dataType);
        Task SaveImage(ProfileImage profileImage);
    }
}
