using DigitalEducation.Entities;
using DigitalEducation.Entities.Users;
using DigitalEducation.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DigitalEducation.Models.Theory;
using Microsoft.EntityFrameworkCore.Internal;

namespace DigitalEducation.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IDbContextWithSets
{
    public DbSet<Problem> Problems { get; set; } = null!;

    public DbSet<Section> Sections { get; set; } = null!;

    public DbSet<Subsection> Subsections { get; set; } = null!;

    public DbSet<ImageFile> ImageFiles { get; set; } = null!;
    
    public DbSet<Theory> Theory { get; set; } = default!;

    public DbSet<TheoryImageFile> TheoryImageFiles => Set<TheoryImageFile>();
    
    public DbSet<ProblemWithAnswer> ProblemsWithAnswer => Set<ProblemWithAnswer>();

    public DbSet<TrainerTheory> TrainerTheories => Set<TrainerTheory>();

    public DbSet<Trainer> Trainers => Set<Trainer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TrainerTheory>().HasKey(t => new { t.TheoryId, t.TrainerId });
        base.OnModelCreating(builder);
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}