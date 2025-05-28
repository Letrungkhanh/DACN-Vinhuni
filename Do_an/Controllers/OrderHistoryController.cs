using Do_an.Models;
using Do_an.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Do_an.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly QlBhqContext _context;

        public OrderHistoryController(QlBhqContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!Functions.IsLogin())
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = _context.TbOrders
                .Include(o => o.TbOrderDetails)
                    .ThenInclude(d => d.Product) // nếu có navigation đến sản phẩm
                .Where(o => o.AccountId == Functions._UserID)
                .OrderByDescending(o => o.CreatedDate)
                .ToList();

            return View(orders);
        }
    }
}

