using System.ComponentModel.DataAnnotations;
namespace LinkVault.DTOs.Note
{
    public class FiltersNoteDTO
    {
        public int? CategoryId { get; set; } 
        public string? SearchWord { get; set; }
        public bool? Pinned { get; set; }
    }
}