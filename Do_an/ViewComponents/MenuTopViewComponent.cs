using Do_an.Models;
using Microsoft.AspNetCore.Mvc;

namespace DACN.ViewComponents
{
	public class MenuTopViewComponent : ViewComponent
	{
		private readonly QlBhqContext _context;
		public MenuTopViewComponent(QlBhqContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = _context.TbMenus.Where(m => (bool)m.IsActive).
				OrderBy(m => m.Position).ToList();
			return await Task.FromResult<IViewComponentResult>(View(items));
		}
	}
}
