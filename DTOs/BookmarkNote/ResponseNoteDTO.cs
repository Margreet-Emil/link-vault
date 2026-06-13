namespace LinkVault.DTOs.BookmarkNote
{
    public class ResponseNoteDTO
    {

        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int BookmarkId { get; set; }

      
    }
}
