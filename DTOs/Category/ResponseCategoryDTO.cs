using System.ComponentModel.DataAnnotations;
namespace LinkVault.DTOs.Category
{
    public class ResponseCategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BookmarksCount { get; set; }
         public int NotesCount { get; set; }    
    }
}
