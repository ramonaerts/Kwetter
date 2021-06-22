using System;
using System.IO;
using System.Threading.Tasks;
using FileManagementService.DAL;
using FileManagementService.Entities;
using FileManagementService.Enums;
using Shared.Messaging;

namespace FileManagementService.Services
{
    public class FileManagementService : IFileManagementService
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly IFileManagementRepository _fileManagementRepository;

        public FileManagementService(IMessagePublisher messagePublisher, IFileManagementRepository fileManagementRepository)
        {
            _messagePublisher = messagePublisher;
            _fileManagementRepository = fileManagementRepository;
        }

        public async Task SaveProfileImage(string id, string image, DataType type)
        {
            var base64 = image.Substring(image.LastIndexOf(',') + 1);
            var uniqueFileName = Guid.NewGuid() + GetFileExtension(base64);
            
            var profileImage = new ProfileImage
            {
                Id = Guid.NewGuid().ToString(),
                DataType = DataType.Profile,
                File = base64,
                Filename = uniqueFileName
            };

            await _fileManagementRepository.SaveImage(profileImage);

            await _messagePublisher.PublishMessageAsync("ProfileImageChangedMessage", new { Id = id, Image = uniqueFileName });
        }

        public void DeleteOldProfileImage(string id, string oldImage, DataType type)
        {
            var filePath = Environment.CurrentDirectory + GetPath(type) + oldImage;

            File.Delete(filePath);
        }

        public bool GetContentOfType(string dataString, DataType type, out byte[] bytes)
        {
            var profileImage = _fileManagementRepository.GetImage(dataString, type);
            if (profileImage == null)
            {
                bytes = null;
                return false;
            }

            bytes = Convert.FromBase64String(profileImage.File);

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
