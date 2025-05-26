using System.Reflection.Metadata;

namespace DOCUMENT_MANAGER.Models
{
    public class DocumentTag
    {
        public int Id { get; set; }
        public string Tag { get; set; }

        public Guid DocumentId { get; set; }
        public UploadedFile Document { get; set; }
    }
}
