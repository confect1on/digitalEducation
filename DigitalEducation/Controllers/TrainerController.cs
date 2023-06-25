using DigitalEducation.Data;
using DigitalEducation.Entities;
using DigitalEducation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EFCore;

namespace DigitalEducation.Controllers;

public class TrainerController : Controller
{
    private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
    private readonly ParallelExtension parallelExtension;
    public TrainerController(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
        parallelExtension = new ParallelExtension(dbContextFactory);
    }
    public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken = default)
    {
        // var sectionTask = parallelExtension.GetDataParallel<Section>(cancellationToken);
        // var subsectionTask = parallelExtension.GetDataParallel<Subsection>(cancellationToken);
        // await Task.WhenAll(sectionTask, subsectionTask);
        // ViewBag.Sections = new SelectList(sectionTask.Result, "Id", "Name");
        // ViewBag.Subsections = new SelectList(subsectionTask.Result
        //         .Where(s => s.SectionId == sectionTask.Result.FirstOrDefault()?.Id), 
        //     "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        CreateTrainerViewModel viewModel, 
        CancellationToken cancellationToken = default)
    {
        await using var problemContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var problemTask = problemContext.Problems
            .Include(p => p.Subsection)
            .Where(p => (viewModel.IsSubsectionNotSpecified || p.SubsectionId == viewModel.SubsectionId) 
                        && p.Subsection!.SectionId == viewModel.SectionId)
            .OrderBy(p => EF.Functions.Random())
            .Take(1)
            .FirstOrDefaultAsync(cancellationToken);

        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        var theoriesTask = context.Theory
            .Where(t => (viewModel.IsSubsectionNotSpecified || t.SectionId == viewModel.SectionId) &&
                        t.SubsectionId == viewModel.SubsectionId)
            .ToListAsync(cancellationToken);

        await Task.WhenAll(theoriesTask, problemTask);
        if (problemTask.Result is null || !theoriesTask.Result.Any())
        {
            ModelState.AddModelError("missingData", 
                "Под ваш запрос не найдены разделы с теорией или практическое задание.");
            viewModel.IsSubsectionNotSpecified = false;
            return View("Index", viewModel);
        }

        var trainerTheories = theoriesTask.Result
            .Select(t => new TrainerTheory
            {
                Theory = t,
            })
            .ToList();
        await context.TrainerTheories.AddRangeAsync(trainerTheories, cancellationToken);
        var trainer = new Trainer
        {
            TrainerTheories = trainerTheories,
            ProblemId = problemTask.Result.Id
        };
        await context.Trainers.AddAsync(trainer, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return RedirectToAction("GetTrainer", "Trainer", new RouteValueDictionary(
            new {trainerId = trainer.Id}));
    }

    [HttpGet]
    public async Task<IActionResult> GetTrainerAsync(int trainerId, CancellationToken cancellationToken = default)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        var trainer = await context.Trainers
            .Include(t => t.TrainerTheories)
            .ThenInclude(t => t.Theory)
            .GetAsync(t => t.Id == trainerId, cancellationToken: cancellationToken);
        return View("Details", trainer);
    }
}