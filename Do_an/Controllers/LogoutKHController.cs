using Do_an.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Do_an.Controllers
{
    public class LogoutKHController : Controller
    {
        public IActionResult Index()
        {
            Functions._UserID = 0;
            Functions._Username = string.Empty;
            Functions._Email = string.Empty;
            Functions._Message = string.Empty;
            Functions._MessageEmail = string.Empty;

            return RedirectToAction("Index", "Home"); 
        }
    }
}
