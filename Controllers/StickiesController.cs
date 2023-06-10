using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StickyNotes.Data;
using StickyNotes.Models;

namespace StickyNotes.Controllers
{
    public class StickiesController : Controller
    {
        private readonly StickyNotesContext _context;

        public StickiesController(StickyNotesContext context)
        {
            _context = context;
        }

        // GET: Stickies
        public async Task<IActionResult> Index()
        {
              return _context.Sticky != null ? 
                          View(await _context.Sticky.ToListAsync()) :
                          Problem("Entity set 'StickyNotesContext.Sticky'  is null.");
        }

        // GET: Stickies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sticky == null)
            {
                return NotFound();
            }

            var sticky = await _context.Sticky
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sticky == null)
            {
                return NotFound();
            }

            return View(sticky);
        }

        // GET: Stickies/Create
        
        public IActionResult Create()
        {

            return View();
        }

        

        // POST: Stickies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Note,position")] Sticky sticky)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sticky);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sticky);
        }

        // GET: Stickies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sticky == null)
            {
                return NotFound();
            }

            var sticky = await _context.Sticky.FindAsync(id);
            if (sticky == null)
            {
                return NotFound();
            }
            return View(sticky);
        }

        // POST: Stickies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Note,position")] Sticky sticky)
        {
            if (id != sticky.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sticky);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StickyExists(sticky.Id))
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
            return View(sticky);
        }

        // GET: Stickies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sticky == null)
            {
                return NotFound();
            }

            var sticky = await _context.Sticky
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sticky == null)
            {
                return NotFound();
            }

            return View(sticky);
        }

        // POST: Stickies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sticky == null)
            {
                return Problem("Entity set 'StickyNotesContext.Sticky'  is null.");
            }
            var sticky = await _context.Sticky.FindAsync(id);
            if (sticky != null)
            {
                _context.Sticky.Remove(sticky);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StickyExists(int id)
        {
          return (_context.Sticky?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
