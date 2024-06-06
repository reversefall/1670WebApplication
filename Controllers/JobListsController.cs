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
    public class JobListsController : Controller
    {
        private readonly _1670WebApplicationContext _context;

        public JobListsController(_1670WebApplicationContext context)
        {
            _context = context;
        }

        // GET: JobLists
        public async Task<IActionResult> Index()
        {
            var _1670WebApplicationContext = _context.JobList.Include(j => j.Category).Include(j => j.Employer);
            return View(await _1670WebApplicationContext.ToListAsync());
        }

        // GET: JobLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobList == null)
            {
                return NotFound();
            }

            var jobList = await _context.JobList
                .Include(j => j.Category)
                .Include(j => j.Employer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobList == null)
            {
                return NotFound();
            }

            return View(jobList);
        }

        // GET: JobLists/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "ID");
            ViewData["EmployerID"] = new SelectList(_context.Employer, "EmployerID", "EmployerID");
            return View();
        }

        // POST: JobLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CategoryID,EmployerID,Title,Description,Required,ApplicationDeadline")] JobList jobList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "ID", jobList.CategoryID);
            ViewData["EmployerID"] = new SelectList(_context.Employer, "EmployerID", "EmployerID", jobList.EmployerID);
            return View(jobList);
        }

        // GET: JobLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobList == null)
            {
                return NotFound();
            }

            var jobList = await _context.JobList.FindAsync(id);
            if (jobList == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "ID", jobList.CategoryID);
            ViewData["EmployerID"] = new SelectList(_context.Employer, "EmployerID", "EmployerID", jobList.EmployerID);
            return View(jobList);
        }

        // POST: JobLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CategoryID,EmployerID,Title,Description,Required,ApplicationDeadline")] JobList jobList)
        {
            if (id != jobList.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobListExists(jobList.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "ID", jobList.CategoryID);
            ViewData["EmployerID"] = new SelectList(_context.Employer, "EmployerID", "EmployerID", jobList.EmployerID);
            return View(jobList);
        }

        // GET: JobLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobList == null)
            {
                return NotFound();
            }

            var jobList = await _context.JobList
                .Include(j => j.Category)
                .Include(j => j.Employer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobList == null)
            {
                return NotFound();
            }

            return View(jobList);
        }

        // POST: JobLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobList == null)
            {
                return Problem("Entity set '_1670WebApplicationContext.JobList'  is null.");
            }
            var jobList = await _context.JobList.FindAsync(id);
            if (jobList != null)
            {
                _context.JobList.Remove(jobList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobListExists(int id)
        {
          return (_context.JobList?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
