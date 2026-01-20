using System.ComponentModel.DataAnnotations;

namespace FruitSimulation.Areas.Admin.ViewModels.Product
{
    public class CreateProductVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        [MaxLength(35)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [Range(0.01, 99999999.99)]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(350)]
        public string Description { get; set; }
    }
}
