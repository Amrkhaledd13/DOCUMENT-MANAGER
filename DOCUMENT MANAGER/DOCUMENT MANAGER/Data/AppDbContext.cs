using DOCUMENT_MANAGER.Models;
using Microsoft.EntityFrameworkCore;

namespace DOCUMENT_MANAGER.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UploadedFile> Documents { get; set; }
        public DbSet<DocumentTag> DocumentTags { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<AccessControlEntry> AccessControlEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentTag>()
                .HasOne(t => t.Document)
                .WithMany(d => d.Tags)
                .HasForeignKey(t => t.DocumentId);

            modelBuilder.Entity<AccessControlEntry>()
                .HasOne(a => a.Document)
                .WithMany(d => d.AccessList)
                .HasForeignKey(a => a.DocumentId);

            modelBuilder.Entity<Folder>()
                .HasOne(f => f.ParentFolder)
                .WithMany()
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
