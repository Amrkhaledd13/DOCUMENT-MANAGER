using DOCUMENT_MANAGER.Data;
using DOCUMENT_MANAGER.Models;
using DOCUMENT_MANAGER.DTOs;
using Microsoft.EntityFrameworkCore;
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
            if (!FileValidator.Validate(request.File, out string error))
                throw new ArgumentException(error);

            var fileId = Guid.NewGuid();
            var extension = Path.GetExtension(request.File.FileName);
            var fileName = $"{fileId}{extension}";
            var savePath = Path.Combine(_uploadDirectory, fileName);

            if (!Directory.Exists(_uploadDirectory))
                Directory.CreateDirectory(_uploadDirectory);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var uploadedFile = new UploadedFile
            {
                Id = fileId,
                FilePath = savePath,
                Title = request.Title,
                Description = request.Description,
                FolderId = request.FolderId,
                Tags = (request.Tags ?? new List<string>()).Select(tag => new DocumentTag
                {
                    DocumentId = fileId,
                    Tag = tag
                }).ToList(),
                UploadedAt = DateTime.UtcNow
            };

            _context.Documents.Add(uploadedFile);
            await _context.SaveChangesAsync();

            return uploadedFile;
        }

        public async Task<List<UploadedFile>> GetAllFilesAsync()
        {
            return await _context.Documents
                .Include(f => f.Tags) 
                .ToListAsync();
        }

    }
}
