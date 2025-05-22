using Do_an.Helpers;
using Do_an.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Do_an.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang") ?? new List<CartItem>();
            int totalQuantity = cart.Sum(x => x.SoLuong);
            double totalPrice = cart.Sum(x => x.ThanhTien);

            ViewBag.TotalQuantity = totalQuantity;
            ViewBag.TotalPrice = totalPrice;

            return View();
        }
    }
}
