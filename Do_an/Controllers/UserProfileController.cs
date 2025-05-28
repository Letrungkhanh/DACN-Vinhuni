using Do_an.Models;
using Do_an.Utilities;
using Do_an.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class UserProfileController : Controller
{
    private readonly QlBhqContext _context;

    public UserProfileController(QlBhqContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
       

        if (!Functions.IsLogin())
            return RedirectToAction("Index", "LoginKH");

        var account = _context.TbAccounts.FirstOrDefault(c => c.AccountId == Functions._UserID);

        if (account == null)
        {
            return NotFound();
        }

        return View(account);
    }
    [HttpGet]
    public IActionResult Edit()
    {
        if (!Functions.IsLogin())
            return RedirectToAction("Index", "LoginKH");

        var account = _context.TbAccounts.FirstOrDefault(x => x.AccountId == Functions._UserID);
        if (account == null) return NotFound();

        var model = new EditProfileViewModel
        {
            AccountId = account.AccountId,
            FullName = account.Username,
            Email = account.Email,
            Phone = account.Phone
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(EditProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var account = _context.TbAccounts.FirstOrDefault(x => x.AccountId == model.AccountId);
        if (account == null) return NotFound();

        account.Username = model.FullName;
        account.Email = model.Email;
        account.Phone = model.Phone;

        if (!string.IsNullOrEmpty(model.NewPassword))
        {
            account.Password = Functions.MD5Password(model.NewPassword);
        }

        _context.SaveChanges();
        ViewBag.Message = "Cập nhật thông tin thành công!";
        return View(model);
    }

}
