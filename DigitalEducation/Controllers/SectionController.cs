using DigitalEducation.Data;
using DigitalEducation.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalEducation.Controllers;

public class SectionController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public SectionController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        return View(await dbContext.Sections.ToListAsync());
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(Section section)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        await dbContext.Sections.AddAsync(section);
        await dbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        var section = await GetSectionById(id);
        if (section is null)
        {
            return NotFound();
        }
        return View(section);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Section section)
    {
        dbContext.Sections.Remove(section);
        await dbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    private async Task<Section?> GetSectionById(int? id)
    {
        if (id is null)
        {
            return null;
        }

        return await dbContext.Sections.FirstOrDefaultAsync(s => s.Id == id);
    }
}