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
        [HttpPost]
        public IActionResult CancelOrder(int id)
        {
            var order = _context.TbOrders.FirstOrDefault(o => o.OrderId == id && o.AccountId == Functions._UserID);
            if (order == null)
            {
                return NotFound();
            }

            // Cho phép hủy bất kỳ lúc nào
            order.OrderStatusId = 5; // Trạng thái huỷ
            _context.SaveChanges();

            TempData["Message"] = "Đơn hàng đã được huỷ thành công.";
            return RedirectToAction("Index");
        }

    }
}

