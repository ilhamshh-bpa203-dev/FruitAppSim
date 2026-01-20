using FruitSimulation.Models.Base;

namespace FruitSimulation.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
