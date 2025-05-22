using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Do_an.Utilities;

namespace Do_an.Areas.Admin.Controllers
{
    public class LogoutController : Controller
    {
        [Area("Admin")]

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
