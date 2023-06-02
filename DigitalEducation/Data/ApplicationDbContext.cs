using DigitalEducation.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalEducation.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Test> Tests { get; set; }
    
    public DbSet<Problem> Problems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}