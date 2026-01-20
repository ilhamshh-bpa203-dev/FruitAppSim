using FruitSimulation.Models;

namespace FruitSimulation.Areas.Admin.ViewModels.Product
{
    public class GetProductVM
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
    }
}
