using Do_an.Models;
using Microsoft.AspNetCore.Mvc;
using Do_an.Utilities;
using Do_an.Models;
using Do_an.Models;

namespace Do_an.Controllers
{
  
    public class RegisterController : Controller
    {
        private readonly QlBhqContext _context;
        public RegisterController(QlBhqContext context)
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

            var check = _context.TbAccounts.Where(m => m.Email == user.Email).FirstOrDefault();
            if (check != null)
            {
                Functions._MessageEmail = "Duplicate Email!";
                return RedirectToAction("Index", "Register");
            }
            Functions._MessageEmail = string.Empty;
            user.Password = Functions.MD5Password(user.Password);
            _context.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "LoginKH");
        }
    }
}