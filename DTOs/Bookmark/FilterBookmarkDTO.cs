
using System.ComponentModel.DataAnnotations;
namespace LinkVault.DTOs.Bookmark
{
    public class FilterBookmarkDTO
    {
        public string? SearchTerm { get; set; }
      
        public int? CategoryId { get; set; }
        public bool? IsArchived { get; set; }
        public bool? IsFavorite { get; set; }
    }
}
