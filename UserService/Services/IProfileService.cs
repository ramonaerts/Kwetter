using System.Threading.Tasks;
using UserService.Messages.API;
using UserService.Messages.Broker;
using UserService.Models;

namespace UserService.Services
{
    public interface IProfileService
    {
        Task<Profile> GetProfileByUsername(string username);
        Task<Profile> GetProfileById(string id);
        Task<bool> EditProfile(EditProfileMessage message);
        Task EditProfileImage(ProfileImageChangedMessage message);
    }
}
