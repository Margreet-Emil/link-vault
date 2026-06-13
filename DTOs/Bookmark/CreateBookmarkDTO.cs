using System.ComponentModel.DataAnnotations;

namespace LinkVault.DTOs.Bookmark
{
    public class CreateBookmarkDTO
    {
        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "URL is Required")]
        [MaxLength(500)]
        [Url(ErrorMessage = "Must be a valid URL")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Category is Required")]
        public int CategoryId { get; set; }
    }
}