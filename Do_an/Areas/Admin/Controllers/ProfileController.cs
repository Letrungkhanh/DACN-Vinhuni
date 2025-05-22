using Do_an.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Do_an.Areas.Admin.Controllers
{
	public class ProfileController : Controller
	{
		public IActionResult Index()
		{
		
			return View();
			
		}
	}
}
