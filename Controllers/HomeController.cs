using System.Diagnostics;
using System.Threading.Tasks;
using FruitSimulation.Data;
using FruitSimulation.Models;
using FruitSimulation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitSimulation.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.Include(p=>p.Category).ToListAsync();

            HomeVM homeVM = new HomeVM()
            {
                Products = products,
            };

            return View(homeVM);
        }

    
    }
}
