using DOCUMENT_MANAGER.Services;
using DOCUMENT_MANAGER.Models;
using Microsoft.AspNetCore.Mvc;

namespace DOCUMENT_MANAGER.Controllers
{
        [ApiController]
        [Route("api/folders")]
    public class FolderController : ControllerBase
    {
        
            private readonly FolderService _folderService;

            public FolderController(FolderService folderService)
            {
                _folderService = folderService;
            }

            [HttpPost]
            public async Task<IActionResult> CreateFolder([FromQuery] string name, [FromQuery] Guid? parentId)
            {
                var folder = await _folderService.CreateFolderAsync(name, parentId);
                return Ok(folder);
            }

            [HttpPut("{id}/rename")]
            public async Task<IActionResult> RenameFolder(Guid id, [FromQuery] string newName)
            {
                var success = await _folderService.RenameFolderAsync(id, newName);
                if (!success) return NotFound();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteFolder(Guid id)
            {
                var success = await _folderService.DeleteFolderAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }

            [HttpGet]
            public async Task<IActionResult> GetAllFolders()
            {
                var folders = await _folderService.GetAllFoldersAsync();
                return Ok(folders);
            }
        
    }
}
