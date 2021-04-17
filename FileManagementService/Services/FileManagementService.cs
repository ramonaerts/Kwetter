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
        //Image will be in base64 | image name will be GUID + file extension | DataType will be Profile for now.
        public void SaveUserImage(string image, string imageName, DataType type)
        {
            var filePath = Environment.CurrentDirectory + GetPath(type) + imageName;

            File.WriteAllBytes(filePath, Convert.FromBase64String(image));
        }

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
