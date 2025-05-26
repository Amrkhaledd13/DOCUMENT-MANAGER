namespace DOCUMENT_MANAGER.Models
{
    public class UploadedFile
    {
        public Guid Id { get; set; }                    
        public string FilePath { get; set; }           
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadedAt { get; set; }


        public List<DocumentTag> Tags { get; set; } = new();
        public List<AccessControlEntry> AccessList { get; set; } = new();
        public Guid? FolderId { get; set; }
        public Folder? Folder { get; set; }
    }
}
