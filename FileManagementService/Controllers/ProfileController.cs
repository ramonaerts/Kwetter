using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementService.Enums;
using FileManagementService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace FileManagementService.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IFileManagementService _fileManagementService;

        public ProfileController(IFileManagementService fileManagementService)
        {
            _fileManagementService = fileManagementService;
        }

        [HttpGet]
        [Route("images/{profileImage}")]
        public IActionResult GetProfileImage(string profileImage)
        {
            if (!_fileManagementService.GetContentOfType(profileImage, DataType.Profile, out var imageBytes))
                return new NotFoundResult();

            if (!new FileExtensionContentTypeProvider().TryGetContentType(profileImage, out var contentType))
                contentType = "application/octet-stream";

            return new FileContentResult(imageBytes, contentType);
        }
    }
}
