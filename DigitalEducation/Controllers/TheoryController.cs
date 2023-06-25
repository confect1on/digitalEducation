using DigitalEducation.Data;
using DigitalEducation.Entities;
using DigitalEducation.Interfaces;
using DigitalEducation.Models;
using DigitalEducation.Models.Theory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using Saritasa.Tools.EFCore;

namespace DigitalEducation.Controllers;

public class TheoryController : Controller
{
    private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
    private readonly IImageFileStore imageFileStore;
    private readonly ApplicationDbContext dbContext;

    public TheoryController(IDbContextFactory<ApplicationDbContext> dbContextFactory, IImageFileStore imageFileStore)
    {
        dbContext = dbContextFactory.CreateDbContext();
        this.dbContextFactory = dbContextFactory;
        this.imageFileStore = imageFileStore;
    }

    public async Task<IActionResult> Index()
    {
        return View(await dbContext.Theory
            .Include(t => t.Section)
            .Include(t => t.Subsection)
            .ToListAsync());
    }

    public async Task<IActionResult> Get(int theoryId)
    {
        var theory = await dbContext.Theory
            .Include(t => t.TheoryImageFiles)
            .GetAsync(t => t.Id == theoryId);
        return View("Details", theory);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        var sectionsTask = GetDataParallel<Section>(cancellationToken);
        var subsectionsTask = GetDataParallel<Subsection>(cancellationToken);

        await Task.WhenAll(sectionsTask, subsectionsTask);
        ViewBag.Sections = new SelectList(
            sectionsTask.Result, "Id", "Name");
        ViewBag.Subsections = new SelectList(
            subsectionsTask.Result.Where(s => s.SectionId == sectionsTask.Result.FirstOrDefault()?.Id), "Id", "Name");
        return View();
    }

    public async Task<IActionResult> CreateAsync(TheoryCreateViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var files = new List<ImageFile>();
        foreach (var formFile in viewModel.FormFileCollection)
        {
            var fileStream = formFile.OpenReadStream();
            files.Add(await imageFileStore.SaveFileAsync(fileStream, cancellationToken));
            await fileStream.DisposeAsync();
        }
        
        var theory = new Theory
        {
            Theme = viewModel.Title,
            SectionId = viewModel.SectionId,
            SubsectionId = !viewModel.IsSubsectionNotSpecified ? viewModel.SubsectionId : null,
        };
        var theoryImageFiles = files
            .Select(imageFile => new TheoryImageFile
            {
                ImageFileId = imageFile.Id,
                Theory = theory
            })
            .ToList();
        theory.TheoryImageFiles.AddRange(theoryImageFiles);
        await dbContext.Theory.AddAsync(theory, cancellationToken);
        await dbContext.TheoryImageFiles.AddRangeAsync(theory.TheoryImageFiles, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ListAsync(CancellationToken cancellationToken = default)
    {
        var sectionsTask = GetDataParallel<Section>(cancellationToken);
        var subsectionsTask = GetDataParallel<Subsection>(cancellationToken);
        var theoriesTask = GetDataParallel<Theory>(cancellationToken);

        await Task.WhenAll(sectionsTask, subsectionsTask, theoriesTask);
        return View(new TheoryListModel
        {
            Sections = sectionsTask.Result,
            Subsections = subsectionsTask.Result,
            Theories = theoriesTask.Result
        });
    }

    private async Task<List<T>> GetDataParallel<T>(CancellationToken cancellationToken = default) where T: class
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<T>().ToListAsync(cancellationToken);
    }
}