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
    public class ApplicationsController : Controller
    {
        private readonly _1670WebApplicationContext _context;

        public ApplicationsController(_1670WebApplicationContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var _1670WebApplicationContext = _context.Application.Include(a => a.JobList).Include(a => a.JobSeeker);
            return View(await _1670WebApplicationContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .Include(a => a.JobList)
                .Include(a => a.JobSeeker)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["JobListingID"] = new SelectList(_context.JobList, "ID", "ID");
            ViewData["JobSeekerID"] = new SelectList(_context.Set<JobSeeker>(), "ID", "ID");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JobSeekerID,JobListingID,Status,Resume,CoverLetter,SelfIntroduction")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobListingID"] = new SelectList(_context.JobList, "ID", "ID", application.JobListingID);
            ViewData["JobSeekerID"] = new SelectList(_context.Set<JobSeeker>(), "ID", "ID", application.JobSeekerID);
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["JobListingID"] = new SelectList(_context.JobList, "ID", "ID", application.JobListingID);
            ViewData["JobSeekerID"] = new SelectList(_context.Set<JobSeeker>(), "ID", "ID", application.JobSeekerID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JobSeekerID,JobListingID,Status,Resume,CoverLetter,SelfIntroduction")] Application application)
        {
            if (id != application.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ID))
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
            ViewData["JobListingID"] = new SelectList(_context.JobList, "ID", "ID", application.JobListingID);
            ViewData["JobSeekerID"] = new SelectList(_context.Set<JobSeeker>(), "ID", "ID", application.JobSeekerID);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .Include(a => a.JobList)
                .Include(a => a.JobSeeker)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Application == null)
            {
                return Problem("Entity set '_1670WebApplicationContext.Application'  is null.");
            }
            var application = await _context.Application.FindAsync(id);
            if (application != null)
            {
                _context.Application.Remove(application);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
          return (_context.Application?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
