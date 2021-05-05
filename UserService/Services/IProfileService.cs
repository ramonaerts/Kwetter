using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;
using UserService.Messages.API;

namespace UserService.Services
{
    public interface IProfileService
    {
        Task<User> GetProfileByUsername(string username);
        Task<User> GetProfileById(string id);
        Task<bool> EditProfile(EditProfileMessage message);
    }
}
