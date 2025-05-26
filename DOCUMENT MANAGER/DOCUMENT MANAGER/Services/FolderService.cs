using DOCUMENT_MANAGER.Data;
using DOCUMENT_MANAGER.Models;
using Microsoft.EntityFrameworkCore;

namespace DOCUMENT_MANAGER.Services
{
    public class FolderService
    {
        private readonly AppDbContext _context;

        public FolderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Folder> CreateFolderAsync(string name, Guid? parentId = null)
        {
            var folder = new Folder
            {
                Name = name,
                ParentFolderId = parentId
            };

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return folder;
        }

        public async Task<bool> RenameFolderAsync(Guid folderId, string newName)
        {
            var folder = await _context.Folders.FindAsync(folderId);
            if (folder == null) return false;

            folder.Name = newName;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFolderAsync(Guid folderId)
        {
            var folder = await _context.Folders.FindAsync(folderId);
            if (folder == null) return false;

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Folder>> GetAllFoldersAsync()
        {
            return await _context.Folders.ToListAsync();
        }
    }
}
