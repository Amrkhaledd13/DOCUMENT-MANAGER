using System.ComponentModel.DataAnnotations;

namespace DOCUMENT_MANAGER.DTOs
{
    public class FileUploadRequest
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> Tags { get; set; }

        public Guid? FolderId { get; set; } // Optional
    }
}
