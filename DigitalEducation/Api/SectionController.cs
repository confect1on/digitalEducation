using DigitalEducation.Data;
using DigitalEducation.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EFCore;

namespace DigitalEducation.Api;

[ApiController]
[Route("api/[controller]")]
public class SectionController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public SectionController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Section>>> Get()
    {
        return await dbContext.Sections.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Section>> Get(int id)
    {
        return await dbContext.Sections.GetAsync(s => s.Id == id);
    }
}