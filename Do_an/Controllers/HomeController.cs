using Do_an.Models;
using Do_an.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Do_an.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly QlBhqContext _context;
		public HomeController(ILogger<HomeController> logger, QlBhqContext context)
		{
			_context = context;
			_logger = logger;
		}

		public IActionResult Index()

		{
			//if(!Functions.IsLogin())
   //             return RedirectToAction("Index", "LoginKH");
            //var products = _context.TbProducts.OrderBy(p => Guid.NewGuid()).Take(3).ToList();
            return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public IActionResult Logout() 
		{
			Functions._UserID = 0;
            Functions._Username = string.Empty;
			Functions._Email = string.Empty;
			Functions._MessageEmail = string.Empty;
			return RedirectToAction("Index", "Home");
        }
		
	}
}
