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
        void SaveUserImage(string image, DataType type);
    }
}
