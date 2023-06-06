using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalEducation.Data;
using DigitalEducation.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DigitalEducation.Controllers
{
    public class SubsectionController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubsectionController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            return View(await dbContext.Subsections
                .Include(x => x.Section)
                .ToListAsync(cancellationToken));
        }
        
        public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
        {
            ViewBag.Sections = new SelectList(await dbContext.Sections.ToListAsync(cancellationToken), "Id", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(
            Subsection subsection,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await dbContext.Subsections.AddAsync(subsection, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken = default)
        {
            var subsection = await GetSubsectionById(id, cancellationToken);
            if (subsection is null)
            {
                return NotFound();
            }
            return View(subsection);
        }

        public async Task<IActionResult> DeleteAsync(Subsection subsection,
            CancellationToken cancellationToken = default)
        {
            dbContext.Remove(subsection);
            await dbContext.SaveChangesAsync(cancellationToken);
            return RedirectToAction("Index");
        }

        private async Task<Subsection?> GetSubsectionById(int? id, CancellationToken cancellationToken = default)
        {
            if (id is null)
            {
                return null;
            }

            return await dbContext.Subsections
                .Include(s => s.Section)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
    }
}