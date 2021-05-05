using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;
using UserService.DAL;
using UserService.Entities;
using UserService.Messages.API;

namespace UserService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserContext _userContext;
        private readonly IUserService _userService;
        private readonly IMessagePublisher _messagePublisher;

        public ProfileService(UserContext userContext, IUserService userService, IMessagePublisher messagePublisher)
        {
            _userContext = userContext;
            _userService = userService;
            _messagePublisher = messagePublisher;
        }

        public async Task<User> GetProfileByUsername(string username)
        {
            return await _userContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetProfileById(string id)
        {
            return await _userContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> EditProfile(EditProfileMessage message)
        {
            var user = await GetProfileById(message.Id);

            if (user == null) return false;

            if (user.Email != message.Email)
            {
                if (_userService.VerifyUniqueEmail(message.Email)) return false;
            }

            user.Email = message.Email;
            user.Nickname = message.Nickname;

            await _userContext.SaveChangesAsync();

            return true;
        }
    }
}
