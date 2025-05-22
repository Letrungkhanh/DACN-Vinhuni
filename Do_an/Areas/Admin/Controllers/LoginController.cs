using Do_an.Areas.Admin.Models;
using Do_an.Models;
using Do_an.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Do_an.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly QlBhqContext _context;
        public LoginController(QlBhqContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Models.AdminUser user)
        {
            if (user == null)
            {
                return NotFound();
            }
            string pw = Functions.MD5Password(user.Password);

            var check = _context.AdminUsers.Where(m => (m.Email == user.Email) && (m.Password == pw)).FirstOrDefault();
            if (check == null)
            {
                Functions._Message = "Invalid UserName or Password!";
                return RedirectToAction("Index", "Login");
            }
            Functions._Message = string.Empty;
            Functions._UserID = check.UserId;
            Functions._Username = string.IsNullOrEmpty(check.UserName) ? string.Empty : check.UserName;
            Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;

            return RedirectToAction("Index", "Home");
        }
    }
}