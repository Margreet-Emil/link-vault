using LinkVault.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkVault.Data.EntityConfigrations
{
    public class BookmarkNoteConfigration : IEntityTypeConfiguration<BookmarkNote>
    {
        public void Configure(EntityTypeBuilder<BookmarkNote> builder)
        {
          builder.HasKey(bn => bn.Id);
            builder.Property(bn => bn.Content).IsRequired().HasMaxLength(500);
            builder.Property(bn => bn.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
           

            builder.HasOne(b => b.Bookmark)
                .WithMany(c => c.BookmarkNotes)

                .HasForeignKey(b => b.BookmarkId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

