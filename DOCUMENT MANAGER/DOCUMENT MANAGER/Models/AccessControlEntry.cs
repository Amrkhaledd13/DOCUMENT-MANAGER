using System.Reflection.Metadata;
using System.Security;

namespace DOCUMENT_MANAGER.Models
{


    public class AccessControlEntry
    {

        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid UserId { get; set; }
        public Permission Permissions { get; set; }

        public UploadedFile Document { get; set; }

    }
}
