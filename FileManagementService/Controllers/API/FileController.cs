using System;
using System.Collections.Generic;
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
            var id = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            await _fileManagementService.SaveUserImage(id, message.Image, DataType.Profile);

            return ApiResult.Success("Image successfully changed");
        }
    }
}
