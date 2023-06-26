using DigitalEducation.Data;
using DigitalEducation.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EFCore;

namespace DigitalEducation.Api;

public class ProblemController : ControllerBase
{
    private readonly ApplicationDbContext applicationDbContext;

    public ProblemController(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    [HttpGet("[action]/")]
    public async Task<ActionResult<bool>> Validate(int problemWithAnswerId)
    {
        var problemWithAnswer = await applicationDbContext.ProblemsWithAnswer
            .Include(p => p.Problem)
            .GetAsync(p => p.Id == problemWithAnswerId);
        return problemWithAnswer.Problem!.Answer == problemWithAnswer.UserAnswer;
    }

    public async Task<ActionResult<ProblemWithAnswer>> GetWithAnswer(int problemWithAnswerId)
    {
        return await applicationDbContext.ProblemsWithAnswer
            .Include(p => p.Problem)
            .GetAsync(p => p.Id == problemWithAnswerId);
    }
}