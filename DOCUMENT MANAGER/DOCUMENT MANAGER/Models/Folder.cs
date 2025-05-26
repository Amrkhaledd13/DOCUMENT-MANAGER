namespace DOCUMENT_MANAGER.Models
{
    public class Folder
    {
        public Guid Id { get; set; } = Guid.NewGuid();   
        public string Name { get; set; }               
        public Guid? ParentFolderId { get; set; }

        public Folder? ParentFolder { get; set; }
    }
}
