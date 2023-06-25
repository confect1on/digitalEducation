namespace DigitalEducation.Entities;

public class ProblemWithAnswer
{
    public int Id { get; set; }
    
    public int ProblemId { get; set; }
    
    public Problem? Problem { get; set; }
    
    required public string UserAnswer { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}