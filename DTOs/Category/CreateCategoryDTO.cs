using System.ComponentModel.DataAnnotations;

namespace LinkVault.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string ? Description { get; set; }

    }
}
