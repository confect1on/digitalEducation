using DigitalEducation.Data;
using DigitalEducation.Entities;
using DigitalEducation.Interfaces;
using DigitalEducation.Models;
using DigitalEducation.Models.Problem;
using DigitalEducation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DigitalEducation.Controllers;

public class ProblemController : Controller
{
    private readonly ApplicationDbContext dbContext;
    private readonly IImageFileStore imageFileStore;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ProblemController(ApplicationDbContext dbContext, IImageFileStore imageFileStore)
    {
        this.dbContext = dbContext;
        this.imageFileStore = imageFileStore;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var problems = await dbContext.Problems
            .Include(p => p.Subsection)
            .ToListAsync();
        return View(problems);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Subsections = new SelectList(await dbContext.Subsections.ToListAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(ProblemViewModel problemViewModel,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var picturePath = problemViewModel.Picture is not null ?
                await imageFileStore.SaveFileAsync(problemViewModel.Picture.OpenReadStream(), cancellationToken)
                : null;
            var solutionPicturePath = 
                await imageFileStore.SaveFileAsync(problemViewModel.SolutionPicture.OpenReadStream(), cancellationToken);
            var problem = new Problem
            {
                Answer = problemViewModel.Answer,
                ProblemImageFile = picturePath,
                Question = problemViewModel.Question,
                SubsectionId = problemViewModel.SubsectionId,
                SolutionImageFile = solutionPicturePath
            };
            await dbContext.Problems.AddAsync(problem, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            ModelState.AddModelError("exception", e.Message);
            return View("Index");
        }
    }
}