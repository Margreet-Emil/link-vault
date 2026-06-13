namespace LinkVault.Models
{
    public class Bookmark : Baseentity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsArchived { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<BookmarkNote> BookmarkNotes { get; set; }
    }
}
