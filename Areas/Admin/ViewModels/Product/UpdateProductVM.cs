using System.ComponentModel.DataAnnotations;

namespace FruitSimulation.Areas.Admin.ViewModels.Product
{
    public class UpdateProductVM
    {
        public string? ImageUrl { get; set; }
        public IFormFile? Photo { get; set; }
        [MaxLength(35)]
        [MinLength(3)]
        [Required]
        public string? Name { get; set; }
        [Range(0.01, 99999999.99)]
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        [MaxLength(350)]
        [MinLength(1)]
        [Required]
        public string? Description { get; set; }
    }
}
