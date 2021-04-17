using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementService.Enums;
using FileManagementService.Messages.Broker;
using FileManagementService.Services;

namespace FileManagementService.MessageHandlers
{
    public class NewProfileImageMessageHandler : IMessageHandler<NewProfileImage>
    {
        private IFileManagementService _fileManagementService;

        public NewProfileImageMessageHandler(IFileManagementService fileManagementService)
        {
            _fileManagementService = fileManagementService;
        }

        public async Task HandleMessageAsync(string messageType, NewProfileImage message)
        {
            _fileManagementService.SaveUserImage(message.Image, message.ImageName, DataType.Profile);
        }
    }
}
