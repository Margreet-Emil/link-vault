using System.ComponentModel.DataAnnotations;
namespace LinkVault.DTOs.Note
{
    public class UpdateNoteDTO
    {
        [Required(ErrorMessage = "Note Title is Required")]
        [MaxLength(100, ErrorMessage = "reached max length for note title")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Note Content is Required")]
        [MaxLength(1000, ErrorMessage = "reached max length for note body")]
        public string? Content { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}