using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileManagementService.Enums;

namespace FileManagementService.Services
{
    public class FileManagementService : IFileManagementService
    {
        public bool GetContentOfType(string dataString, DataType type, out byte[] bytes)
        {
            bytes = new byte[0];
            var path = GetPath(type);

            if (!File.Exists(Path.Combine(Environment.CurrentDirectory + path + dataString))) return false;

            bytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory + path + dataString));

            return true;
        }

        private static string GetPath(DataType type)
        {
            switch (type)
            {
                case DataType.Profile:
                    return "/Content/Profileimages/";
                default:
                    return null;
            }
        }
    }
}
