using System;
using System.IO;
using System.Threading.Tasks;
using FileManagementService.Enums;
using Shared.Messaging;

namespace FileManagementService.Services
{
    public class FileManagementService : IFileManagementService
    {
        private readonly IMessagePublisher _messagePublisher;

        public FileManagementService(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public async Task SaveProfileImage(string id, string image, DataType type)
        {
            var base64 = image.Substring(image.LastIndexOf(',') + 1);
            var uniqueFileName = Guid.NewGuid() + GetFileExtension(base64);
            var filePath = Environment.CurrentDirectory + GetPath(type) + uniqueFileName;

            await _messagePublisher.PublishMessageAsync("ProfileImageChangedMessage", new { Id = id, Image = uniqueFileName });

            File.WriteAllBytes(filePath, Convert.FromBase64String(base64));
        }

        public void DeleteOldProfileImage(string id, string oldImage, DataType type)
        {
            var filePath = Environment.CurrentDirectory + GetPath(type) + oldImage;

            File.Delete(filePath);
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
