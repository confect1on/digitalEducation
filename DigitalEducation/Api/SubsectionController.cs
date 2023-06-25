using DigitalEducation.Api.Models;
using DigitalEducation.Data;
using DigitalEducation.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EFCore;

namespace DigitalEducation.Api;
[ApiController]
[Route("api/[controller]")]
public class SubsectionController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public SubsectionController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subsection>>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Subsections.ToListAsync(cancellationToken: cancellationToken);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Subsection>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Subsections.GetAsync(s => s.Id == id, cancellationToken);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Subsection>>> SearchAsync(
        [FromQuery]SearchSubsectionsQuery query,
        CancellationToken cancellationToken = default)
    {
        var subsections = dbContext.Subsections.AsQueryable();
        if (query.SectionId.HasValue)
        {
            subsections = subsections.Where(s => s.SectionId == query.SectionId.Value);
        }

        return await subsections.ToListAsync(cancellationToken);
    }
}