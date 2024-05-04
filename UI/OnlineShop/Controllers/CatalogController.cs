using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Models;

    using ViewModels;

    public class CatalogController : Controller
    {
        private ApplicationDbContext context;

        private List<Product> products;

        private readonly UserManager<User> _userManager;

        public CatalogController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var result = context.Product.ToList();
            return View(result);
        }

        public IActionResult CreateProduct() => View();

        public IActionResult Catalog()
        {
            var allProducts = context.Product.ToList();

            return View(allProducts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product { Name = model.Name, Cost = model.Cost };

                var result = await context.Product.AddAsync(product);

                await context.SaveChangesAsync();

                if (result != null)
                {
                    return RedirectToAction("Catalog");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product product = await context.Product.FirstOrDefaultAsync(x => x.Id == id);

            context.Product.Remove(product);
            await context.SaveChangesAsync();

            return RedirectToAction("Catalog");
        }

        public async Task<IActionResult> EditProduct(int? id)
        {
            Product? product = await context.Product.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            EditProductViewModel model = new EditProductViewModel() { Id = product.Id, Name = product.Name, Cost = product.Cost };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                Product? prod = await context.Product.FirstOrDefaultAsync(x => x.Id == product.Id);

                if (prod != null)
                {
                    prod.Name = product.Name;
                    prod.Cost = product.Cost;

                    var result = context.Product.Update(prod);
                    await context.SaveChangesAsync();
                    
                    return RedirectToAction("Catalog");
                }
            }

            return View(product);
        }
        
        public async Task<IActionResult> OrderProduct(int? id)
        {
            var user = await _userManager.GetUserAsync(this.User);

            var order = new Orders
            {
                ProductId = id.Value,
                UserId = user.Id
            };

            var res = await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            if (res != null)
            {
                return new JsonResult(new { Success = true });
            }
            else
            {
                return new JsonResult(new { Success = false });
            }
        }
    }
}
