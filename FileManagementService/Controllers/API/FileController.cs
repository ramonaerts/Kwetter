using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FileManagementService.Enums;
using FileManagementService.Messages.API;
using FileManagementService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;

namespace FileManagementService.Controllers.API
{
    [Authorize]
    [ApiController]
    [Route("api/{controller}")]
    public class FileController : ControllerBase
    {
        private readonly IFileManagementService _fileManagementService;

        public FileController(IFileManagementService fileManagementService)
        {
            _fileManagementService = fileManagementService;
        }

        [Authorize(Roles = "User,Moderator,Admin")]
        [HttpPost("edit")]
        public async Task<ApiResult> EditProfilePicture(EditProfileImageMessage message)
        {
            try
            {
                var id = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

                //if (message.OldImage != null) _fileManagementService.DeleteOldProfileImage(id, message.NewImage, DataType.Profile);

                await _fileManagementService.SaveProfileImage(id, message.Image, DataType.Profile);

                return ApiResult.Success("Image successfully changed");
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }
    }
}
