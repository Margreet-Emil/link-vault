
using LinkVault.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
  


namespace LinkVault.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public  AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }
    
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookmarkNote> BookmarkNotes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
