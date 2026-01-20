using FruitSimulation.Models.Base;

namespace FruitSimulation.Models
{
    public class Product:BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category  { get; set; }
    }
}
