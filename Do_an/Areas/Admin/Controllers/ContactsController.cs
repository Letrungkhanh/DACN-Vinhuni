using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Do_an.Models;

namespace Do_an.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly QlBhqContext _context;

        public ContactsController(QlBhqContext context)
        {
            _context = context;
        }

        // GET: Admin/Contacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbContacts.ToListAsync());
        }

        // GET: Admin/Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbContact = await _context.TbContacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (tbContact == null)
            {
                return NotFound();
            }

            return View(tbContact);
        }

        // GET: Admin/Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,Name,Phone,Email,Message,IsRead,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] TbContact tbContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbContact);
        }

        // GET: Admin/Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbContact = await _context.TbContacts.FindAsync(id);
            if (tbContact == null)
            {
                return NotFound();
            }
            return View(tbContact);
        }

        // POST: Admin/Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,Name,Phone,Email,Message,IsRead,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] TbContact tbContact)
        {
            if (id != tbContact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbContactExists(tbContact.ContactId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbContact);
        }

        // GET: Admin/Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbContact = await _context.TbContacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (tbContact == null)
            {
                return NotFound();
            }

            return View(tbContact);
        }

        // POST: Admin/Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbContact = await _context.TbContacts.FindAsync(id);
            if (tbContact != null)
            {
                _context.TbContacts.Remove(tbContact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbContactExists(int id)
        {
            return _context.TbContacts.Any(e => e.ContactId == id);
        }
    }
}
