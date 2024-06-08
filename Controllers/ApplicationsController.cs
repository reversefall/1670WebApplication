using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1670WebApplication.Data;
using _1670WebApplication.Models;
using Microsoft.AspNetCore.Hosting;

namespace _1670WebApplication.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly _1670WebApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ApplicationsController(_1670WebApplicationContext dbcontext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dbcontext;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var applications = await _context.Application.ToListAsync();
            return View(applications);
            //var _1670WebApplicationContext = _context.Application.Include(a => a.JobList).Include(a => a.JobSeeker);
            //return View(await _1670WebApplicationContext.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public async Task<IActionResult> Add(Application model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Application application = new Application
                {
                    Status = model.Status,
                    Resume = model.Resume,
                    CoverLetter = model.CoverLetter,
                    SelfIntroduction = model.SelfIntroduction,
                    JobList = model.JobList,
                    JobSeeker = model.JobSeeker,
                };
                _context.Add(application);
            }
            // ViewData["JobListingID"] = new SelectList(_context.JobList, "ID", "ID", application.JobListingID);
            //ViewData["JobSeekerID"] = new SelectList(_context.Set<JobSeeker>(), "ID", "ID", application.JobSeekerID);
            return View();
        }
        private string UploadedFile(Application model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }*/

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
        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["JobListingID"] = new SelectList(_context.JobList, "ID", "JobTitle");
            ViewData["JobSeekerID"] = new SelectList(_context.JobSeekers, "ID", "FullName");
            return View();
        }


        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        /*public async Task<IActionResult> Create([Bind("ID,JobSeekerID,JobListingID,Status,Resume,CoverLetter,SelfIntroduction")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JobSeekerID,JobListingID,Status,Resume,CoverLetter,SelfIntroduction")] Application model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Application application = new Application
                {
                    Status = model.Status,
                    Resume = model.Resume,
                    CoverLetter = model.CoverLetter,
                    SelfIntroduction = model.SelfIntroduction,
                    JobListID = model.JobListID,
                    JobSeekersID = model.JobSeekersID,
                    ImagePath = uniqueFileName


                };
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobListingID"] = new SelectList(_context.JobList, "ID", "JobTitle", model.JobListID);
            ViewData["JobSeekerID"] = new SelectList(_context.JobSeekers, "ID", "FullName", model.JobSeekersID);
            return View(model);
        }
        private string UploadedFile(Application model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
            ViewData["JobListingID"] = new SelectList(_context.JobList, "ID", "ID", application.JobListID);
            ViewData["JobSeekerID"] = new SelectList(_context.Set<JobSeekers>(), "ID", "ID", application.JobSeekersID);
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
            ViewData["JobListingID"] = new SelectList(_context.JobList, "ID", "ID", application.JobListID);
            ViewData["JobSeekerID"] = new SelectList(_context.Set<JobSeekers>(), "ID", "ID", application.JobSeekersID);
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
