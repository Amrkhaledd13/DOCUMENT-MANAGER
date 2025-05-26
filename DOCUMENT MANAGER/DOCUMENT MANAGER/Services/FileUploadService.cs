using DOCUMENT_MANAGER.Data;
using DOCUMENT_MANAGER.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DOCUMENT_MANAGER.Services
{
    public class FileUploadService
    {
        private readonly string _uploadDirectory = "UploadedFiles";
        private readonly AppDbContext _context;

        public FileUploadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UploadedFile> UploadAsync(FileUploadRequest request)
        {
            // Step 1: Validate file type
            if (!FileValidator.Validate(request.File, out string error))
                throw new ArgumentException(error);

            // Step 2: Create unique file name and path
            var fileId = Guid.NewGuid();
            var extension = Path.GetExtension(request.File.FileName);
            var fileName = $"{fileId}{extension}";
            var savePath = Path.Combine(_uploadDirectory, fileName);

            // تأكد إن الفولدر موجود
            if (!Directory.Exists(_uploadDirectory))
                Directory.CreateDirectory(_uploadDirectory);

            // Step 3: Save file to disk
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            // Step 4: Build metadata object
            var uploadedFile = new UploadedFile
            {
                Id = fileId,
                FilePath = savePath,
                Title = request.Title,
                Description = request.Description,
                Tags = (request.Tags ?? new List<string>()).Select(tag => new DocumentTag
                {
                    DocumentId = fileId,
                    Tag = tag
                }).ToList(),
                UploadedAt = DateTime.UtcNow
            };

            // Step 5: Save metadata to DB
            _context.Documents.Add(uploadedFile);
            await _context.SaveChangesAsync();

            return uploadedFile;
        }
    }
}
