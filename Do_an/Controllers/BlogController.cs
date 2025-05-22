using Do_an.Models;
using Do_an.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Do_an.Controllers
{
	public class BlogController : Controller
	{
		private readonly QlBhqContext _context;
		public BlogController(QlBhqContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var blogs = _context.TbBlogs.ToList();
			return View(blogs);
		}
		[Route("/blog/{alias}-{id}.html")]
        public async Task<IActionResult> Details(int? id)
		{
            if (id == null || _context.TbBlogs == null)
			{
				return NotFound();
			}
			var blogs = await _context.TbBlogs.Include(m =>m.TbBlogComments).FirstOrDefaultAsync(m => m.BlogId == id);
			if (blogs == null)
			{
				return NotFound();
            }
			return View(blogs);

        }

        [HttpPost]
        public IActionResult AddComment(TbBlogComment model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.IsActive = false; // hoặc true nếu không cần kiểm duyệt

                _context.TbBlogComments.Add(model);
                _context.SaveChanges();
            }


            var blog = _context.TbBlogs.FirstOrDefault(p => p.BlogId == model.BlogId);
            if (blog != null)
            {
                var alias = blog.Alias; // bạn cần chắc chắn Product có trường Alias
                return Redirect($"/blog/{alias}-{blog.BlogId}.html");
            }

            return RedirectToAction("Index", "Home"); // fallback nếu không tìm thấy
        }


    }
}
