using System.ComponentModel.DataAnnotations;

namespace LinkVault.DTOs.Bookmark
{
    public class ResponseBookmarkDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsArchived { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NotesCount { get; set; }
        public string  CategoryName { get; set; }
    }
}
