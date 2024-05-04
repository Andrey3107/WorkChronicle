namespace OnlineShop.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Models;

    using ViewModels;

    public class OrdersController : Controller
    {
        private ApplicationDbContext context;

        public OrdersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await context.Orders
                .Join(
                    context.Product,
                    o => o.ProductId,
                    p => p.Id,
                    (o, p) => new { o.OrderId, o.UserId, p.Name, p.Cost }
                )
                .Join(
                    context.Users,
                    o => o.UserId,
                    u => u.Id,
                    (o, u) => new { o.OrderId, o.Name, o.Cost, u.UserName, u.Email }
                )
                .Select(x => new OrderViewModel
                {
                    Id = x.OrderId,
                    ProductName = x.Name,
                    ProductCost = x.Cost,
                    ProductOwner = x.UserName,
                    OwnerEmail = x.Email
                })
                .ToListAsync();

            return View(result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var existsOrder = await context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);

            context.Orders.Remove(existsOrder);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
