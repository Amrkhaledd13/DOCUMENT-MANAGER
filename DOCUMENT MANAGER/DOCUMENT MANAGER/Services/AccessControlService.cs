using DOCUMENT_MANAGER.Data;
using DOCUMENT_MANAGER.Models;
using Microsoft.EntityFrameworkCore;

namespace DOCUMENT_MANAGER.Services
{
    public class AccessControlService
    {
        private readonly AppDbContext _context;

        public AccessControlService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SetPermissionsAsync(Guid documentId, Guid userId, Permission permissions)
        {
            var entry = await _context.AccessControlEntries
                .FirstOrDefaultAsync(e => e.DocumentId == documentId && e.UserId == userId);

            if (entry == null)
            {
                _context.AccessControlEntries.Add(new AccessControlEntry
                {
                    DocumentId = documentId,
                    UserId = userId,
                    Permissions = permissions
                });
            }
            else
            {
                entry.Permissions = permissions;
                _context.AccessControlEntries.Update(entry);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Permission> GetPermissionsAsync(Guid documentId, Guid userId)
        {
            var entry = await _context.AccessControlEntries
                .FirstOrDefaultAsync(e => e.DocumentId == documentId && e.UserId == userId);

            return entry?.Permissions ?? Permission.None;
        }

        public async Task<bool> HasPermissionAsync(Guid documentId, Guid userId, Permission requiredPermission)
        {
            var entry = await _context.AccessControlEntries
                .FirstOrDefaultAsync(e => e.DocumentId == documentId && e.UserId == userId);

            return entry != null && entry.Permissions.HasFlag(requiredPermission);
        }
    }
}
