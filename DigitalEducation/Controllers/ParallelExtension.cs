using DigitalEducation.Data;
using Microsoft.EntityFrameworkCore;

namespace DigitalEducation.Controllers;

public class ParallelExtension
{
    private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

    public ParallelExtension(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<List<T>> GetDataParallel<T>(CancellationToken cancellationToken = default) where T: class
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<T>().ToListAsync(cancellationToken);
    }
}