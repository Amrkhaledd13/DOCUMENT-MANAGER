using DOCUMENT_MANAGER.Data;
using DOCUMENT_MANAGER.Models;
using Microsoft.EntityFrameworkCore;

namespace DOCUMENT_MANAGER.Services
{
    public class TaggingService
    {
        private readonly AppDbContext _context;

        public TaggingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTagsAsync(Guid documentId, List<string> tags)
        {
            var document = await _context.Documents
                .Include(d => d.Tags)
                .FirstOrDefaultAsync(d => d.Id == documentId);

            if (document == null) return false;

            foreach (var tag in tags)
            {
                if (!document.Tags.Any(t => t.Tag == tag))
                {
                    document.Tags.Add(new DocumentTag
                    {
                        DocumentId = documentId,
                        Tag = tag
                    });
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveTagAsync(Guid documentId, string tag)
        {
            var tagEntity = await _context.DocumentTags
                .FirstOrDefaultAsync(t => t.DocumentId == documentId && t.Tag == tag);

            if (tagEntity == null) return false;

            _context.DocumentTags.Remove(tagEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReplaceTagsAsync(Guid documentId, List<string> newTags)
        {
            var existingTags = _context.DocumentTags
                .Where(t => t.DocumentId == documentId);

            _context.DocumentTags.RemoveRange(existingTags);

            var newTagEntities = newTags.Select(tag => new DocumentTag
            {
                DocumentId = documentId,
                Tag = tag
            });

            await _context.DocumentTags.AddRangeAsync(newTagEntities);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetTagsAsync(Guid documentId)
        {
            return await _context.DocumentTags
                .Where(t => t.DocumentId == documentId)
                .Select(t => t.Tag)
                .ToListAsync();
        }
    }
}
