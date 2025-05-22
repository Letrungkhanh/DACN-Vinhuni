using Do_an.Models;
using Microsoft.AspNetCore.Mvc;
using Do_an.Helpers;
using Do_an.ViewModel;
using Do_an.Utilities;

namespace Do_an.Controllers
{
    public class CartController : Controller
    {
        private readonly QlBhqContext _context;

        public CartController(QlBhqContext context)
        {
            _context = context;
        }

        const string CART_KEY = "GioHang"; // thống nhất key session

        // Lấy giỏ hàng từ session, nếu chưa có thì tạo mới
        public List<CartItem> Carts => HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "LoginKH");

            return View(Carts);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var giohang = Carts;
            var item = giohang.SingleOrDefault(p => p.Mahh == id);

            if (item == null)
            {
                var hanghoa = _context.TbProducts.SingleOrDefault(p => p.ProductId == id);
                if (hanghoa != null)
                {
                    item = new CartItem
                    {
                        Mahh = hanghoa.ProductId,
                        TenHH = hanghoa.Title,
                        Hinh = hanghoa.Image,
                        DonGia = hanghoa.Price ?? 0,
                        SoLuong = quantity,
                        Product = hanghoa  // Nếu CartItem có property Product thì gán luôn
                    };
                    giohang.Add(item);
                }
            }
            else
            {
                item.SoLuong += quantity;
            }

            // Lưu lại vào session
            HttpContext.Session.Set(CART_KEY, giohang);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var giohang = Carts;
            var item = giohang.SingleOrDefault(p => p.Mahh == id);

            if (item != null)
            {
                giohang.Remove(item);
                HttpContext.Session.Set(CART_KEY, giohang);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cartItems = Carts;
            if (!cartItems.Any())
                return RedirectToAction("Index", "Home");

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult CheckoutConfirm(string CustomerName, string Phone, string Address, string Note)
        {
            var cartItems = Carts;

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            string orderCode = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");

            var order = new TbOrder
            {
                Code = orderCode,
                CustomerName = CustomerName,
                Phone = Phone,
                Address = Address,
                TotalAmount = cartItems.Sum(x => (int)x.ThanhTien),
                Quanlity = cartItems.Sum(x => x.SoLuong),
                OrderStatusId = 1,
                CreatedDate = DateTime.Now,
                CreatedBy = "Customer"
            };

            _context.TbOrders.Add(order);
            _context.SaveChanges();

            int orderId = order.OrderId;

            foreach (var item in cartItems)
            {
                var orderDetail = new TbOrderDetail
                {
                    OrderId = orderId,
                    ProductId = item.Product.ProductId,
                    Price = item.Product.Price,
                    Quantity = item.SoLuong
                };
                _context.TbOrderDetails.Add(orderDetail);
            }

            _context.SaveChanges();

            // Xóa giỏ hàng trong session sau khi đặt hàng xong
            HttpContext.Session.Remove(CART_KEY);

            return RedirectToAction("Thankyou");
        }

        public IActionResult Thankyou()
        {
            return View();
        }
    }
}
