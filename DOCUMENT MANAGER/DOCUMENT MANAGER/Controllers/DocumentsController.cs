using DOCUMENT_MANAGER.Models;
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

            public DocumentsController(FileUploadService fileUploadService, TaggingService taggingService)
            {
                _fileUploadService = fileUploadService;
                _taggingService = taggingService;
            }

            [HttpPost("upload")]
            public async Task<IActionResult> Upload([FromForm] FileUploadRequest request)
            {
                var uploadedFile = await _fileUploadService.UploadAsync(request);
                if (request.Tags != null && request.Tags.Any())
                {
                    await _taggingService.AddTagsAsync(uploadedFile.Id, request.Tags);
                }
                return Ok(uploadedFile);
            }

            [HttpPost("{documentId}/tags")]
            public async Task<IActionResult> AddTags(Guid documentId, [FromBody] List<string> tags)
            {
                var result = await _taggingService.AddTagsAsync(documentId, tags);
                return result ? Ok("Tags added successfully.") : NotFound("Document not found.");
            }

            [HttpDelete("{documentId}/tags/{tag}")]
            public async Task<IActionResult> RemoveTag(Guid documentId, string tag)
            {
                var result = await _taggingService.RemoveTagAsync(documentId, tag);
                return result ? Ok("Tag removed successfully.") : NotFound("Tag or document not found.");
            }

            [HttpPut("{documentId}/tags")]
            public async Task<IActionResult> ReplaceTags(Guid documentId, [FromBody] List<string> tags)
            {
                var result = await _taggingService.ReplaceTagsAsync(documentId, tags);
                return result ? Ok("Tags replaced successfully.") : NotFound("Document not found.");
            }

            [HttpGet("{documentId}/tags")]
            public async Task<IActionResult> GetTags(Guid documentId)
            {
                var tags = await _taggingService.GetTagsAsync(documentId);
                return Ok(tags);
            }
        }
    
}
