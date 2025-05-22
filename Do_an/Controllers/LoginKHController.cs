using Do_an.Models;
using Do_an.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Do_an.Controllers
{
    public class LoginKHcontroller : Controller
    {
        private readonly QlBhqContext _context;
        public LoginKHcontroller(QlBhqContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Models.TbAccount user)
        {
            if (user == null)
            {
                return NotFound();
            }
            string pw = Functions.MD5Password(user.Password);

            var check = _context.TbAccounts.Where(m => (m.Email == user.Email) && (m.Password == pw)).FirstOrDefault();
            if (check == null)
            {
                Functions._Message = "Invalid UserName or Password!";
                return RedirectToAction("Index", "LoginKH");
            }
            Functions._Message = string.Empty;
            Functions._UserID = check.AccountId;
            Functions._Username = string.IsNullOrEmpty(check.Username) ? string.Empty : check.Username;
            Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;

            return RedirectToAction("Index", "Home");
        }
    }
}
