using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1670WebApplication.Data;
using _1670WebApplication.Models;

namespace _1670WebApplication.Controllers
{
    public class JobSeekersController : Controller
    {
        private readonly _1670WebApplicationContext _context;

        public JobSeekersController(_1670WebApplicationContext context)
        {
            _context = context;
        }

        // GET: JobSeekers
        public async Task<IActionResult> Index()
        {
              return _context.JobSeeker != null ? 
                          View(await _context.JobSeeker.ToListAsync()) :
                          Problem("Entity set '_1670WebApplicationContext.JobSeeker'  is null.");
        }

        // GET: JobSeekers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobSeeker == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeeker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }

        // GET: JobSeekers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobSeekers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountID,FirstName,LastName,Phone,Email,Resume,CoverLetter")] JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobSeeker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobSeeker);
        }

        // GET: JobSeekers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobSeeker == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeeker.FindAsync(id);
            if (jobSeeker == null)
            {
                return NotFound();
            }
            return View(jobSeeker);
        }

        // POST: JobSeekers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountID,FirstName,LastName,Phone,Email,Resume,CoverLetter")] JobSeeker jobSeeker)
        {
            if (id != jobSeeker.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobSeeker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobSeekerExists(jobSeeker.ID))
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
            return View(jobSeeker);
        }

        // GET: JobSeekers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobSeeker == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeeker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }

        // POST: JobSeekers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobSeeker == null)
            {
                return Problem("Entity set '_1670WebApplicationContext.JobSeeker'  is null.");
            }
            var jobSeeker = await _context.JobSeeker.FindAsync(id);
            if (jobSeeker != null)
            {
                _context.JobSeeker.Remove(jobSeeker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobSeekerExists(int id)
        {
          return (_context.JobSeeker?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
