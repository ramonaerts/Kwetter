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
        public void SaveUserImage(string image, DataType type)
        {
            var base64 = image.Substring(image.LastIndexOf(',') + 1);

            var filePath = Environment.CurrentDirectory + GetPath(type) + Guid.NewGuid() + GetFileExtension(base64);

            File.WriteAllBytes(filePath, Convert.FromBase64String(base64));
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
            return type switch
            {
                DataType.Profile => "/Content/Profileimages/",
                _ => null
            };
        }

        private static string GetFileExtension(string base64)
        {
            var data = base64.Substring(0, 5);

            return data.ToUpper() switch
            {
                "IVBOR" => ".png",
                "/9J/4" => ".jpg",
                _ => null,
            };
        }
    }
}
