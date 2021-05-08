using System;
using System.Collections.Generic;
using System.Linq;
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

        [AllowAnonymous]
        [HttpPost("edit")]
        public ApiResult EditProfilePicture(EditProfileImageMessage message)
        {
            _fileManagementService.SaveUserImage(message.Image, DataType.Profile);

            return ApiResult.Success("Image successfully changed");
        }
    }
}
