using DigitalEducation.Data;

namespace DigitalEducation.Controllers;

public class TrainerController
{
    private readonly ApplicationDbContext dbContext;

    public TrainerController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    
}