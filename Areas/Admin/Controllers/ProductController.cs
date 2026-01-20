using FruitSimulation.Areas.Admin.ViewModels.Product;
using FruitSimulation.Data;
using FruitSimulation.Models;
using FruitSimulation.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FruitSimulation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<GetProductVM> getProductVMs = await _context.Products
                .Include(p => p.Category)
                .Select(p => new GetProductVM()
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                }).ToListAsync();

            return View(getProductVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Category = await _context.Categories.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM createProductVM)
        {
            ViewBag.Category = await _context.Categories.ToListAsync();

            bool isExistsProduct = await _context.Products.AnyAsync(p => p.Name == createProductVM.Name);

            bool isExistsCategory = await _context.Categories.AnyAsync(c => c.Id == createProductVM.CategoryId);

            if (!ModelState.IsValid)
            {
                return View(createProductVM);
            }

            if (!createProductVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError(nameof(CreateProductVM.Photo), "Invalid image type..");
                return View(createProductVM);
            }

            if (isExistsProduct)
            {
                ModelState.AddModelError(nameof(CreateProductVM.Name), "Product already exists..");
                return View(createProductVM);
            }

            if (!isExistsCategory)
            {
                ModelState.AddModelError(nameof(CreateProductVM.CategoryId), "Category not exists or empty..");
                return View(createProductVM);
            }

            var fileName = await createProductVM.Photo.CreateFileAsync(_env.WebRootPath, "img");

            Product product = new Product()
            {
                Name = createProductVM.Name,
                ImageUrl = fileName,
                Price = createProductVM.Price,
                Description = createProductVM.Description,
                CategoryId = createProductVM.CategoryId,
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 0) return BadRequest();

            ViewBag.Category = await _context.Categories.ToListAsync();

            Product product = await _context.Products.Include(P => P.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            UpdateProductVM updateProductVM = new UpdateProductVM()
            {
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
            };

            return View(updateProductVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVM updateProductVM,int? id)
        {
            if (id is null || id < 0) return BadRequest();

            ViewBag.Category = await _context.Categories.ToListAsync();

            Product product = await _context.Products.Include(P => P.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            updateProductVM.ImageUrl = product.ImageUrl;


            if (!ModelState.IsValid)
            {
                return View(updateProductVM);
            }

            if (updateProductVM.Photo is not null)
            {
                if (!updateProductVM.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError(nameof(CreateProductVM.Photo), "Invalid image type..");
                    return View(updateProductVM);
                }

                var fileName = await updateProductVM.Photo.CreateFileAsync(_env.WebRootPath, "img");
                product.ImageUrl = fileName;
                updateProductVM.ImageUrl.DeleteFile(_env.WebRootPath, "img");
            }

            product.Price=updateProductVM.Price.Value;
            product.Name = updateProductVM.Name;
            product.Description = updateProductVM.Description;
            product.CategoryId=updateProductVM.CategoryId.Value;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 0) return BadRequest();

            ViewBag.Category = await _context.Categories.ToListAsync();

            Product product = await _context.Products.Include(P => P.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            
            product.ImageUrl.DeleteFile(_env.WebRootPath, "img");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
