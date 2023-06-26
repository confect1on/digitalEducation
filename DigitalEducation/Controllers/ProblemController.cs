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
using Saritasa.Tools.EFCore;

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
            var pictureImageFile = problemViewModel.Picture is not null 
                ? await imageFileStore.SaveFileAsync(problemViewModel.Picture.OpenReadStream(), cancellationToken)
                : null;
            var solutionImageFile = 
                await imageFileStore.SaveFileAsync(problemViewModel.SolutionPicture.OpenReadStream(), cancellationToken);
            var hintImageFile = problemViewModel.HintPicture is not null
                ? await imageFileStore.SaveFileAsync(problemViewModel.HintPicture.OpenReadStream(), cancellationToken)
                : null;

            var problem = new Problem
            {
                Answer = problemViewModel.Answer,
                ProblemImageFile = pictureImageFile,
                Question = problemViewModel.Question,
                SubsectionId = problemViewModel.SubsectionId,
                SolutionImageFile = solutionImageFile,
                HintImageFile = hintImageFile
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

    [HttpGet("{problemId:int}")]
    public async Task<IActionResult> Get(int problemId)
    {
        var problem = await dbContext.Problems.GetAsync(p => p.Id == problemId);
        return View("Details", problem);
    }
}