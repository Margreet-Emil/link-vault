
using System.Diagnostics.Eventing.Reader;

namespace LinkVault.Models
{
    public class BookmarkNote : Baseentity
    {
        public string Content { get; set; }

        public int BookmarkId { get; set; }
        public Bookmark Bookmark { get; set; } = null;
    }
}