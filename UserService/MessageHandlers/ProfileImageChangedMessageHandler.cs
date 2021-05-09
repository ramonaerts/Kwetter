using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using UserService.Messages.Broker;
using UserService.Services;

namespace UserService.MessageHandlers
{
    public class ProfileImageChangedMessageHandler : IMessageHandler<ProfileImageChangedMessage>
    {
        private readonly IProfileService _profileService;

        public ProfileImageChangedMessageHandler(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task HandleMessageAsync(string messageType, ProfileImageChangedMessage message)
        {
            await _profileService.EditProfileImage(message);
        }
    }
}
