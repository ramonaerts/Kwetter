using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;
using UserService.DAL;
using UserService.Entities;

namespace UserService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserContext _userContext;
        private readonly IMessagePublisher _messagePublisher;

        public ProfileService(IMessagePublisher messagePublisher, UserContext userContext)
        {
            _messagePublisher = messagePublisher;
            _userContext = userContext;
        }

        public async Task<User> getUserByUsername(string username)
        {
            return await _userContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
