using DOCUMENT_MANAGER.Models;
using DOCUMENT_MANAGER.DTOs;
using DOCUMENT_MANAGER.Services;
using Microsoft.AspNetCore.Mvc;

namespace DOCUMENT_MANAGER.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly FileUploadService _fileUploadService;
        private readonly TaggingService _taggingService;
        private readonly FolderService _folderService;


        public DocumentsController(FileUploadService fileUploadService, TaggingService taggingService, FolderService folderService)
        {
            _fileUploadService = fileUploadService;
            _taggingService = taggingService;
            _folderService = folderService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] FileUploadRequest request)
        {
            try
            {
                var uploadedFile = await _fileUploadService.UploadAsync(request);

                var response = new
                {
                    FileId = uploadedFile.Id,
                    Title = uploadedFile.Title,
                    Description = uploadedFile.Description,
                    UploadedAt = uploadedFile.UploadedAt,
                    Tags = uploadedFile.Tags.Select(t => t.Tag).ToList(),
                    FilePath = uploadedFile.FilePath
                };
                if (request.FolderId !=  null) 
                _folderService.addfile(request.FolderId, uploadedFile);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllFiles()
        {
            var files = await _fileUploadService.GetAllFilesAsync();

            var response = files.Select(file => new
            {
                FileId = file.Id,
                Title = file.Title,
                Description = file.Description,
                UploadedAt = file.UploadedAt,
                Tags = file.Tags.Select(t => t.Tag).ToList(),
                FilePath = file.FilePath 
            }).ToList();

            return Ok(response);
        }


    }
}
