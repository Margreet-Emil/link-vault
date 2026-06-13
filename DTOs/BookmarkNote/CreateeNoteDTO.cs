using System.ComponentModel.DataAnnotations;

namespace LinkVault.DTOs.BookmarkNote
{
    public class CreateeNoteDTO
    {
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;
    }
}
