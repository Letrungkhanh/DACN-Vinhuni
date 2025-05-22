using Do_an.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACN.ViewComponents
{
	public class ProductViewComponent : ViewComponent
	{
		private readonly QlBhqContext _context;
		public ProductViewComponent(QlBhqContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = _context.TbProducts.Include(p => p.CategoryProduct).
				Where(p => (bool)p.IsActive).Where(m => m.IsNew);
			return await Task.FromResult<IViewComponentResult>
				(View(items.OrderBy(p => p.Price).ToList()));
		}

	}
}
