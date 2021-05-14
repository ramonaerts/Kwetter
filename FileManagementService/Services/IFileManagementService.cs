using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementService.Enums;

namespace FileManagementService.Services
{
    public interface IFileManagementService
    {
        bool GetContentOfType(string dataString, DataType type, out byte[] bytes);
        Task SaveProfileImage(string id, string image, DataType type);
        void DeleteOldProfileImage(string id, string oldImage, DataType type);
    }
}
