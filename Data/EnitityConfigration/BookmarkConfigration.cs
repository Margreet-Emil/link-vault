using LinkVault.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkVault.Data.EntityConfigrations
{
    public class BookmarkConfigrations : IEntityTypeConfiguration<Bookmark>
    {
     
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Url).IsRequired().HasMaxLength(1000);
            builder.HasIndex(b => b.Url).IsUnique();

            builder.Property(b => b.Title).IsRequired().HasMaxLength(100);
            builder.Property(b => b.IsFavorite).HasDefaultValue(false);
            builder.Property(b => b.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(b => b.IsArchived).HasDefaultValue(false);

            builder.HasOne(b => b.Category)
                  .WithMany(c => c.Bookmark)
                  .HasForeignKey(b => b.CategoryID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}