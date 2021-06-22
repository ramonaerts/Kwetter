using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;
using UserService.DAL;
using UserService.Entities;
using UserService.Messages.API;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;
        private readonly IMessagePublisher _messagePublisher;

        public UserService(UserContext userContext, IMessagePublisher messagePublisher)
        {
            _userContext = userContext;
            _messagePublisher = messagePublisher;
        }

        public async Task<bool> RegisterUser(RegisterMessage message)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = message.Email,
                Username = message.Username,
                Nickname = message.Username,
                Image = "default.png",
                Location = "",
                FollowersCount = 0,
                FollowingCount = 0,
                Verified = false
            };

            _userContext.Add(user);
            _userContext.SaveChanges();

            await _messagePublisher.PublishMessageAsync("NewUserMessage", new { Id = user.Id, Email = user.Email, Username = user.Username, Password = message.Password });
            await _messagePublisher.PublishMessageAsync("NewProfileMessage",
                new { Id = user.Id, Username = user.Username, Nickname = user.Nickname, Image = user.Image });

            return true;
        }

        public async Task<bool> CreateAdmin(RegisterMessage message)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = message.Email,
                Username = message.Username,
                Nickname = message.Username,
                Image = "default.png",
                Location = "",
                FollowersCount = 0,
                FollowingCount = 0,
                Verified = true
            };

            _userContext.Add(user);
            _userContext.SaveChanges();

            await _messagePublisher.PublishMessageAsync("NewAdminMessage", new { Id = user.Id, Email = user.Email, Username = user.Username, Password = message.Password });
            await _messagePublisher.PublishMessageAsync("NewProfileMessage",
                new { Id = user.Id, Username = user.Username, Nickname = user.Nickname, Image = user.Image });

            return true;
        }

        public bool VerifyPasswords(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        public bool VerifyUniqueEmail(string email)
        {
            return _userContext.Users.Any(e => e.Email == email);
        }

        public async Task<bool> ForgetUser(string id)
        {
            var exists = await VerifyIfUserExists(id);
            if (!exists) return false;

            _userContext.Users.RemoveRange(_userContext.Users.Where(u => u.Id == id));
            await _userContext.SaveChangesAsync();

            await _messagePublisher.PublishMessageAsync("ForgetUserMessage", new {Id = id});

            return true;
        }

        public async Task<bool> VerifyIfUserExists(string id)
        {
            return await _userContext.Users.AnyAsync(e => e.Id == id);
        }
    }
}
