using Do_an.Models;
using Do_an.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Do_an.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly QlBhqContext _context;

        public HomeController(QlBhqContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var totalRevenue = _context.TbOrders.Sum(o => o.TotalAmount ?? 0);
            ViewBag.TongDoanhThu = totalRevenue;
            ViewBag.TotalUsers = _context.TbAccounts.Count();
            ViewBag.TotalProduct = _context.TbProducts.Count();
            ViewBag.TotalOrder = _context.TbOrders.Count();

            var bestSellers = _context.TbOrderDetails
       .GroupBy(od => od.ProductId)
       .Select(g => new
       {
           ProductId = g.Key,
           TotalSold = g.Sum(od => od.Quantity ?? 0)
       })
       .OrderByDescending(x => x.TotalSold)
       .Take(5)
       .Join(_context.TbProducts,
             bs => bs.ProductId,
             p => p.ProductId,
             (bs, p) => new
             {
                 p.ProductId,
                 p.Title,
                 p.Image,
                 p.Price,
                 bs.TotalSold
             })
       .ToList();

            ViewBag.BestSellers = bestSellers;

            return View();
           // return View(); // Đây là Index.cshtml
        }
        //public IActionResult TopProduct()
        //{
        //    var bestSellers = _context.TbOrderDetails
        //        .GroupBy(od => od.ProductId)
        //        .Select(g => new
        //        {
        //            ProductId = g.Key,
        //            TotalSold = g.Sum(od => od.Quantity ?? 0)
        //        })
        //        .OrderByDescending(x => x.TotalSold)
        //        .Take(5)
        //        .Join(_context.TbProducts,
        //              bs => bs.ProductId,
        //              p => p.ProductId,
        //              (bs, p) => new
        //              {
        //                  p.ProductId,
        //                  p.Title,
        //                  p.Image,
        //                  p.Price,
        //                  bs.TotalSold
        //              })
        //        .ToList();

        //    ViewBag.BestSellers = bestSellers;

        //    return View();
        //}

    }
}
