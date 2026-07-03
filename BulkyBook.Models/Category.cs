using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public String Name { get; set; } = String.Empty;

        [Range(0,100,ErrorMessage = "Range must be between 0 and 100")]

        public int? DisplayOrder { get; set; }


    }
}
