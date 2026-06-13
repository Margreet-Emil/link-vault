using System.ComponentModel.DataAnnotations;
namespace LinkVault.DTOs.Note
{
    public class ResponseNoteDTO
    {
        public int NoteId { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public bool IsPinned { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}