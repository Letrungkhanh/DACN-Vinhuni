using Do_an.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace Do_an.Controllers
{
	public class ContactController : Controller
	{
		private readonly QlBhqContext _context;
		public ContactController(QlBhqContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(string name, string phone, string email, string message)
		{
			try
			{
				TbContact contact = new TbContact();
				contact.Name = name;
				contact.Phone = phone;
				contact.Email = email;
				contact.Message = message;
				contact.CreatedDate = DateTime.Now;
				_context.Add(contact);
				_context.SaveChanges();
				return Json(new { status = true });
			}
			catch
			{
				return Json(new { status = false });

			}
		}
	}
}
