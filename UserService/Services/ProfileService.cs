using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;
using UserService.DAL;
using UserService.Entities;
using UserService.Messages.API;
using UserService.Messages.Broker;
using Profile = UserService.Models.Profile;

namespace UserService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserContext _userContext;
        private readonly IUserService _userService;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IMapper _mapper;

        public ProfileService(UserContext userContext, IUserService userService, IMessagePublisher messagePublisher, IMapper mapper)
        {
            _userContext = userContext;
            _userService = userService;
            _messagePublisher = messagePublisher;
            _mapper = mapper;
        }

        public async Task<Profile> GetProfileByUsername(string username)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            var userModel = _mapper.Map<Profile>(user);

            return userModel;
        }

        public async Task<Profile> GetProfileById(string id)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            var userModel = _mapper.Map<Profile>(user);

            return userModel;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> EditProfile(EditProfileMessage message)
        {
            var user = await GetUserById(message.Id);

            if (user == null) return false;

            if (user.Email != message.Email)
            {
                if (_userService.VerifyUniqueEmail(message.Email)) return false;

                await _messagePublisher.PublishMessageAsync("EmailChangedMessage", new { Id = user.Id, Email = message.Email});
            }

            if (user.Nickname != message.Nickname) await _messagePublisher.PublishMessageAsync("ProfileChangedMessage", new { Id = user.Id, Nickname = message.Nickname });

            user.Email = message.Email;
            user.Nickname = message.Nickname;

            _userContext.Update(user);
            await _userContext.SaveChangesAsync();

            return true;
        }

        public async Task EditProfileImage(ProfileImageChangedMessage message)
        {
            var user = await GetUserById(message.Id);

            user.Image = message.Image;
            _userContext.Update(user);

            await _userContext.SaveChangesAsync();
        }
    }
}
