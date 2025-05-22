using Do_an.Models;
using Do_an.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Do_an.Controllers
{
	public class ProductController : Controller
	{
		private readonly QlBhqContext _context;
		public ProductController(QlBhqContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var products = _context.TbProducts.OrderBy(p => p.Price).ToList();
			return View(products);
		}
		[Route("/product/{alias}-{id}.html")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.TbProducts == null)
			{
				return NotFound();
			}
			var products = await _context.TbProducts.Include(i => i.CategoryProduct).Include(i=>i.TbProductReviews)
				.FirstOrDefaultAsync(m =>m.ProductId ==id);
			if (products == null) 
			{
				return NotFound();
			}
			return View(products);
		}
        [HttpPost]
        [HttpPost]
        public IActionResult AddComment(TbProductReview models)
        {
            if (ModelState.IsValid)
            {
                models.CreatedDate = DateTime.Now;
                models.IsActive = false; // hoặc true nếu không cần kiểm duyệt

                _context.TbProductReviews.Add(models);
                _context.SaveChanges();
            }

            var product = _context.TbProducts.FirstOrDefault(p => p.ProductId == models.ProductId);
            if (product != null)
            {
                return Redirect($"/product/{product.Alias}-{product.ProductId}.html");
            }

            return RedirectToAction("Index", "Home");
        }


    }
}
