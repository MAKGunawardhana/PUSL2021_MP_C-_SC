using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;

namespace Expense_Tracker.Controllers
{
    public class LogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Log
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

 

        // GET: Log/AddOrEdit
        public IActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
                return View(new LogEntity());
            else
                return View(_context.User.Find(id));
        }

        // POST: Log/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,FirstName,LastName,Mobile,Email")] LogEntity logEntity)
        {
            if (ModelState.IsValid)
            {
                if (logEntity.Id == 0)

                    _context.Add(logEntity);

                else

                    _context.Update(logEntity);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logEntity);
        }

 

        // POST: Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'ApplicationDbContext.User' is null.");
            }
            var logEntity = await _context.User.FindAsync(id);
            if (logEntity != null)
            {
                _context.User.Remove(logEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
