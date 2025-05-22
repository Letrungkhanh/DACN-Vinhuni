using Do_an.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Do_an.ViewComponents
{
	public class BlogViewComponent : ViewComponent
	{
		private readonly QlBhqContext _context;
		public BlogViewComponent(QlBhqContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = _context.TbBlogs.Include(m => m.Account)
			   .Where(m => (bool)m.IsActive);
			return await Task.FromResult<IViewComponentResult>
				(View(items.OrderByDescending(m => m.BlogId).ToList())); 
		}
	}
}
