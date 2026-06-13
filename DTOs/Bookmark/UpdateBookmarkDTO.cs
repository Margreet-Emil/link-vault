using System.ComponentModel.DataAnnotations;

namespace LinkVault.DTOs.Bookmark
{
    public class UpdateBookmarkDTO
    {
        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(50)]
        public string Title { get; set; }

     
    }
}
