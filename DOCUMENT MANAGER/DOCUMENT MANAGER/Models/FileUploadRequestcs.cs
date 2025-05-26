namespace DOCUMENT_MANAGER.Models
{
    public class FileUploadRequest
    {
        public IFormFile File { get; set; }           
        public string Title { get; set; }               
        public string Description { get; set; }        
        public List<string> Tags { get; set; }
    }
}
