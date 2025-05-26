namespace DOCUMENT_MANAGER.Services
{
    public class FileValidator
    {

        private static readonly List<string> AllowedExtensions = new()
    {
        ".pdf", ".doc", ".docx", ".xls", ".xlsx"
    };

        private const long MaxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB

        public static bool Validate(IFormFile file, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Check file null
            if (file == null || file.Length == 0)
            {
                errorMessage = "File is empty or not provided.";
                return false;
            }

            // Check extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(extension))
            {
                errorMessage = $"File type '{extension}' is not supported.";
                return false;
            }

            // Check size
            if (file.Length > MaxFileSizeInBytes)
            {
                errorMessage = "File size exceeds the maximum allowed size (10 MB).";
                return false;
            }

            return true;
        }
    }
}
